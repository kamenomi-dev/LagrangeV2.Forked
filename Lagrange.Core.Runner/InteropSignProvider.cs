﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Lagrange.Core.Common;
using Lagrange.Core.Internal.Packets.Service;

namespace Lagrange.Core.NativeAPI.NativeModel.Common;

public class LinuxSignProvider(string? signUrl) : BotSignProvider, IDisposable
{
    private const string Tag = nameof(LinuxSignProvider);

    private readonly HttpClient _client = new();

    private string Url => signUrl ?? $"https://sign.lagrangecore.org/api/sign/{Context.AppInfo.AppClientVersion}";

    private static readonly HashSet<string> WhiteListCommand =
    [
    "trpc.o3.ecdh_access.EcdhAccess.SsoEstablishShareKey",
        "trpc.o3.ecdh_access.EcdhAccess.SsoSecureAccess",
        "trpc.o3.report.Report.SsoReport",
        "MessageSvc.PbSendMsg",
        "wtlogin.trans_emp",
        "wtlogin.login",
        "wtlogin.exchange_emp",
        "trpc.login.ecdh.EcdhService.SsoKeyExchange",
        "trpc.login.ecdh.EcdhService.SsoNTLoginPasswordLogin",
        "trpc.login.ecdh.EcdhService.SsoNTLoginEasyLogin",
        "trpc.login.ecdh.EcdhService.SsoNTLoginPasswordLoginNewDevice",
        "trpc.login.ecdh.EcdhService.SsoNTLoginEasyLoginUnusualDevice",
        "trpc.login.ecdh.EcdhService.SsoNTLoginPasswordLoginUnusualDevice",
        "trpc.login.ecdh.EcdhService.SsoNTLoginRefreshTicket",
        "trpc.login.ecdh.EcdhService.SsoNTLoginRefreshA2",
        "OidbSvcTrpcTcp.0x11ec_1",
        "OidbSvcTrpcTcp.0x758_1", // create group
        "OidbSvcTrpcTcp.0x7c1_1",
        "OidbSvcTrpcTcp.0x7c2_5", // request friend
        "OidbSvcTrpcTcp.0x10db_1",
        "OidbSvcTrpcTcp.0x8a1_7", // request group
        "OidbSvcTrpcTcp.0x89a_0",
        "OidbSvcTrpcTcp.0x89a_15",
        "OidbSvcTrpcTcp.0x88d_0", // fetch group detail
        "OidbSvcTrpcTcp.0x88d_14",
        "OidbSvcTrpcTcp.0x112a_1",
        "OidbSvcTrpcTcp.0x587_74",
        "OidbSvcTrpcTcp.0x1100_1",
        "OidbSvcTrpcTcp.0x1102_1",
        "OidbSvcTrpcTcp.0x1103_1",
        "OidbSvcTrpcTcp.0x1107_1",
        "OidbSvcTrpcTcp.0x1105_1",
        "OidbSvcTrpcTcp.0xf88_1",
        "OidbSvcTrpcTcp.0xf89_1",
        "OidbSvcTrpcTcp.0xf57_1",
        "OidbSvcTrpcTcp.0xf57_106",
        "OidbSvcTrpcTcp.0xf57_9",
        "OidbSvcTrpcTcp.0xf55_1",
        "OidbSvcTrpcTcp.0xf67_1",
        "OidbSvcTrpcTcp.0xf67_5",
        "OidbSvcTrpcTcp.0x6d9_4"
    ];

    public override bool IsWhiteListCommand(string cmd) => WhiteListCommand.Contains(cmd);

    public override async Task<SsoSecureInfo?> GetSecSign(long uin, string cmd, int seq, ReadOnlyMemory<byte> body)
    {
        try
        {
            var payload = new JsonObject
            {
                ["cmd"] = cmd,
                ["seq"] = seq,
                ["src"] = Convert.ToHexString(body.Span),
            };

            var response = await _client.PostAsync(Url, new StringContent(payload.ToJsonString(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return null;

            var content = JsonHelper.Deserialize<Root>(await response.Content.ReadAsStringAsync());
            if (content == null) return null;

            return new SsoSecureInfo
            {
                SecSign = Convert.FromHexString(content.Value.Sign),
                SecToken = Convert.FromHexString(content.Value.Token),
                SecExtra = Convert.FromHexString(content.Value.Extra)
            };
        }
        catch (Exception e)
        {
            Context.LogWarning(Tag, $"Failed to get sign: {e.Message}");
            return null;
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    [Serializable]
    internal class Root
    {
        [JsonPropertyName("value")] public Response Value { get; set; } = new();
    }

    [Serializable]
    internal class Response
    {
        [JsonPropertyName("sign")] public string Sign { get; set; } = string.Empty;

        [JsonPropertyName("token")] public string Token { get; set; } = string.Empty;

        [JsonPropertyName("extra")] public string Extra { get; set; } = string.Empty;
    }
}

internal static partial class JsonHelper
{
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]

    [JsonSerializable(typeof(LinuxSignProvider.Root))]
    [JsonSerializable(typeof(LinuxSignProvider.Response))]

    [JsonSerializable(typeof(JsonObject))]
    [JsonSerializable(typeof(LightApp))]
    private partial class CoreSerializerContext : JsonSerializerContext;

    public static T? Deserialize<T>(string json) where T : class =>
        JsonSerializer.Deserialize(json, typeof(T), CoreSerializerContext.Default) as T;

    public static string Serialize<T>(T value) =>
        JsonSerializer.Serialize(value, typeof(T), CoreSerializerContext.Default);

    public static ReadOnlyMemory<byte> SerializeToUtf8Bytes<T>(T value) =>
        JsonSerializer.SerializeToUtf8Bytes(value, typeof(T), CoreSerializerContext.Default);
}