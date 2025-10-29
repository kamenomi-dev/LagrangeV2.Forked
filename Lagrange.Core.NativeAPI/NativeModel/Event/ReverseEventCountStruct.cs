﻿using System.Runtime.InteropServices;

namespace Lagrange.Core.NativeAPI.NativeModel.Event
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ReverseEventCountStruct
    {
        public ReverseEventCountStruct() { }

        public int BotCaptchaEventCount = 0;
        public int BotFriendRequestEventCount = 0;
        public int BotFriendRecallEventCount = 0;
        public int BotGroupInviteNotificationEventCount = 0;
        public int BotGroupInviteSelfEventCount = 0;
        public int BotGroupJoinNotificationEventCount = 0;
        public int BotGroupMemberDecreaseEventCount = 0;
        public int BotGroupMemberIncreaseEventCount = 0;
        public int BotGroupNudgeEventCount = 0;
        public int BotGroupReactionEventCount = 0;
        public int BotGroupRecallEventCount = 0;
        public int BotLoginEventCount = 0;
        public int BotLogEventCount = 0;
        public int BotMessageEventCount = 0;
        public int BotNewDeviceVerifyEventCount = 0;
        public int BotOnlineEventCount = 0;
        public int BotQrCodeEventCount = 0;
        public int BotQrCodeQueryEventCount = 0;
        public int BotRefreshKeystoreEventCount = 0;
        public int BotSMSEventCount = 0;
    }
}