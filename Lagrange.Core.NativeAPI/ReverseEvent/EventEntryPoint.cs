﻿using System.Runtime.InteropServices;
using Lagrange.Core.NativeAPI.NativeModel.Event;
using Lagrange.Core.NativeAPI.ReverseEvent.Abstract;

namespace Lagrange.Core.NativeAPI.ReverseEvent
{
    public static class EventEntryPoint
    {
        [UnmanagedCallersOnly(EntryPoint = "GetEventCount")]
        public static IntPtr GetEventCount(int index)
        {
            var eventCount = new ReverseEventCountStruct
            {
                BotCaptchaEventCount = Program.Contexts[index].EventInvoker.BotCaptchaEvent.Events.Count,
                BotFriendRequestEventCount = Program.Contexts[index].EventInvoker.BotFriendRequestEvent.Events.Count,
                BotFriendRecallEventCount = Program.Contexts[index].EventInvoker.BotFriendRecallEvent.Events.Count,
                BotGroupInviteNotificationEventCount = Program.Contexts[index].EventInvoker.BotGroupInviteNotificationEvent.Events.Count,
                BotGroupJoinNotificationEventCount = Program.Contexts[index].EventInvoker.BotGroupJoinNotificationEvent.Events.Count,
                BotGroupMemberDecreaseEventCount = Program.Contexts[index].EventInvoker.BotGroupMemberDecreaseEvent.Events.Count,
                BotGroupMemberIncreaseEventCount = Program.Contexts[index].EventInvoker.BotGroupMemberIncreaseEvent.Events.Count,
                BotGroupNudgeEventCount = Program.Contexts[index].EventInvoker.BotGroupNudgeEvent.Events.Count,
                BotGroupReactionEventCount = Program.Contexts[index].EventInvoker.BotGroupReactionEvent.Events.Count,
                BotGroupRecallEventCount = Program.Contexts[index].EventInvoker.BotGroupRecallEvent.Events.Count,
                BotLoginEventCount = Program.Contexts[index].EventInvoker.BotLoginEvent.Events.Count,
                BotLogEventCount = Program.Contexts[index].EventInvoker.BotLogEvent.Events.Count,
                BotMessageEventCount = Program.Contexts[index].EventInvoker.BotMessageEvent.Events.Count,
                BotNewDeviceVerifyEventCount = Program.Contexts[index].EventInvoker.BotNewDeviceVerifyEvent.Events.Count,
                BotOnlineEventCount = Program.Contexts[index].EventInvoker.BotOnlineEvent.Events.Count,
                BotQrCodeEventCount = Program.Contexts[index].EventInvoker.BotQrCodeEvent.Events.Count,
                BotQrCodeQueryEventCount = Program.Contexts[index].EventInvoker.BotQrCodeQueryEvent.Events.Count,
                BotRefreshKeystoreEventCount = Program.Contexts[index].EventInvoker.BotRefreshKeystoreEvent.Events.Count,
                BotSMSEventCount = Program.Contexts[index].EventInvoker.BotSMSEvent.Events.Count
            };

            IntPtr eventCountPtr = Marshal.AllocHGlobal(Marshal.SizeOf<ReverseEventCountStruct>());
            Marshal.StructureToPtr(eventCount, eventCountPtr, false);

            return eventCountPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetCaptchaEvent")]
        public static IntPtr GetCaptchaEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botCaptchaEvent = Program.Contexts[index].EventInvoker.BotCaptchaEvent;

            IntPtr eventPtr = GetEventStructPtr<BotCaptchaEventStruct>(botCaptchaEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotFriendRecallEvent")]
        public static IntPtr GetBotFriendRecallEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botFriendRecallEvent = Program.Contexts[index].EventInvoker.BotFriendRecallEvent;

            IntPtr eventPtr = GetEventStructPtr<BotFriendRecallEventStruct>(botFriendRecallEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotFriendRequestEvent")]
        public static IntPtr GetBotFriendRequestEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botFriendRequestEvent = Program.Contexts[index].EventInvoker.BotFriendRequestEvent;

            IntPtr eventPtr = GetEventStructPtr<BotFriendRequestEventStruct>(botFriendRequestEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupInviteNotificationEvent")]
        public static IntPtr GetBotGroupInviteNotificationEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupInviteNotificationEvent = Program.Contexts[index].EventInvoker.BotGroupInviteNotificationEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupInviteNotificationEventStruct>(botGroupInviteNotificationEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupJoinNotificationEvent")]
        public static IntPtr GetBotGroupJoinNotificationEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupJoinNotificationEvent = Program.Contexts[index].EventInvoker.BotGroupJoinNotificationEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupJoinNotificationEventStruct>(botGroupJoinNotificationEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupMemberDecreaseEvent")]
        public static IntPtr GetBotGroupMemberDecreaseEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupMemberDecreaseEvent = Program.Contexts[index].EventInvoker.BotGroupMemberDecreaseEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupMemberDecreaseEventStruct>(botGroupMemberDecreaseEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupMemberIncreaseEvent")]
        public static IntPtr GetBotGroupMemberIncreaseEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupMemberIncreaseEvent = Program.Contexts[index].EventInvoker.BotGroupMemberIncreaseEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupMemberIncreaseEventStruct>(botGroupMemberIncreaseEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupNudgeEvent")]
        public static IntPtr GetBotGroupNudgeEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupNudgeEvent = Program.Contexts[index].EventInvoker.BotGroupNudgeEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupNudgeEventStruct>(botGroupNudgeEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupReactionEvent")]
        public static IntPtr GetBotGroupReactionEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupReactionEvent = Program.Contexts[index].EventInvoker.BotGroupReactionEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupReactionEventStruct>(botGroupReactionEvent);

            return eventPtr;
        }
       
        [UnmanagedCallersOnly(EntryPoint = "GetBotGroupRecallEvent")]
        public static IntPtr GetBotGroupRecallEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botGroupRecallEvent = Program.Contexts[index].EventInvoker.BotGroupRecallEvent;

            IntPtr eventPtr = GetEventStructPtr<BotGroupReactionEventStruct>(botGroupRecallEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetLoginEvent")]
        public static IntPtr GetLoginEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botLoginEvent = Program.Contexts[index].EventInvoker.BotLoginEvent;

            IntPtr eventPtr = GetEventStructPtr<BotLoginEventStruct>(botLoginEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetBotLogEvent")]
        public static IntPtr GetBotLogEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botLogEvent = Program.Contexts[index].EventInvoker.BotLogEvent;

            IntPtr eventPtr = GetEventStructPtr<BotLogEventStruct>(botLogEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetMessageEvent")]
        public static IntPtr GetMessageEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botMessageEvent = Program.Contexts[index].EventInvoker.BotMessageEvent;

            IntPtr eventPtr = GetEventStructPtr<BotMessageEventStruct>(botMessageEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetNewDeviceVerifyEvent")]
        public static IntPtr GetNewDeviceVerifyEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botNewDeviceVerifyEvent = Program.Contexts[index].EventInvoker.BotNewDeviceVerifyEvent;

            IntPtr eventPtr = GetEventStructPtr<BotNewDeviceVerifyEventStruct>(botNewDeviceVerifyEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetOnlineEvent")]
        public static IntPtr GetOnlineEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botOnlineEvent = Program.Contexts[index].EventInvoker.BotOnlineEvent;

            IntPtr eventPtr = GetEventStructPtr<BotOnlineEventStruct>(botOnlineEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetQrCodeEvent")]
        public static IntPtr GetQrCodeEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botQrCodeEvent = Program.Contexts[index].EventInvoker.BotQrCodeEvent;

            IntPtr eventPtr = GetEventStructPtr<BotQrCodeEventStruct>(botQrCodeEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetQrCodeQueryEvent")]
        public static IntPtr GetQrCodeQueryEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botQrCodeQueryEvent = Program.Contexts[index].EventInvoker.BotQrCodeQueryEvent;

            IntPtr eventPtr = GetEventStructPtr<BotQrCodeQueryEventStruct>(botQrCodeQueryEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetRefreshKeystoreEvent")]
        public static IntPtr GetRefreshKeystoreEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botRefreshKeystoreEvent = Program.Contexts[index].EventInvoker.BotRefreshKeystoreEvent;

            IntPtr eventPtr = GetEventStructPtr<BotRefreshKeystoreEventStruct>(botRefreshKeystoreEvent);

            return eventPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetSMSEvent")]
        public static IntPtr GetSMSEvent(int index)
        {
            if (index >= Program.Contexts.Count)
            {
                return IntPtr.Zero;
            }

            var botSMSEvent = Program.Contexts[index].EventInvoker.BotSMSEvent;

            IntPtr eventPtr = GetEventStructPtr<BotSMSEventStruct>(botSMSEvent);

            return eventPtr;
        }

        private static IntPtr GetEventStructPtr<T>(ReverseEventBase reverseEvent) where T : IEventStruct
        {
            EventArrayStruct result = new EventArrayStruct();

            if (reverseEvent.Events.Count == 0)
            {
                result.Events = IntPtr.Zero;
                result.Count = 0;
            }
            else
            {
                result.Events = Marshal.AllocHGlobal(reverseEvent.Events.Count * Marshal.SizeOf<T>());
                result.Count = reverseEvent.Events.Count;

                for (int i = 0; i < reverseEvent.Events.Count; i++)
                {
                    Marshal.StructureToPtr(
                        reverseEvent.Events[i],
                        result.Events + i * Marshal.SizeOf<T>(),
                        false
                    );
                }

                reverseEvent.Events.Clear();
            }

            IntPtr resultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
            Marshal.StructureToPtr(result, resultPtr, false);
            return resultPtr;
        }
    }
}
