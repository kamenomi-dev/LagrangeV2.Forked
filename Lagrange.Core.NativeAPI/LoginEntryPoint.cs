using System.Runtime.InteropServices;
using System.Text;
using Lagrange.Core.Common.Interface;
using Lagrange.Core.NativeAPI.NativeModel.Common;

namespace Lagrange.Core.NativeAPI
{
    public static class LoginEntryPoint
    {
        [UnmanagedCallersOnly(EntryPoint = "SubmitCaptcha")]
        public static bool SubmitCaptcha(int index, ByteArrayNative ticket, ByteArrayNative randStr)
        {
            if (Program.Contexts.Count <= index)
            {
                return false;
            }

            var ticketData = ticket.ToByteArrayWithoutFree();
            var randStrData = randStr.ToByteArrayWithoutFree();

            BotContext context = Program.Contexts[index].BotContext;
            return context.SubmitCaptcha(Encoding.UTF8.GetString(ticketData), Encoding.UTF8.GetString(randStrData));
        }

        [UnmanagedCallersOnly(EntryPoint = "SubmitSMSCode")]
        public static bool SubmitSMSCode(int index, ByteArrayNative code)
        {
            if (Program.Contexts.Count <= index)
            {
                return false;
            }

            var codeData = code.ToByteArrayWithoutFree();

            BotContext context = Program.Contexts[index].BotContext;
            return context.SubmitSMSCode(Encoding.UTF8.GetString(codeData));
        }
    };
};