using System.Runtime.InteropServices;
using System.Text;
using Lagrange.Core.Common.Entity;
using Lagrange.Core.Common.Interface;
using Lagrange.Core.NativeAPI.NativeModel.Common;
using Lagrange.Core.NativeAPI.NativeModel.Event;
using Lagrange.Core.NativeAPI.NativeModel.Message;

namespace Lagrange.Core.NativeAPI
{
    public static class OperateGroupEntryPoint
    {
        [UnmanagedCallersOnly(EntryPoint = "GetGroupList")]
        public static IntPtr GetGroupList(int index, bool refresh /*= false*/)
        {
            if (Program.Contexts.Count <= index)
            {
                return IntPtr.Zero;
            }

            var context = Program.Contexts[index].BotContext;
            var groups = context.FetchGroups(refresh).GetAwaiter().GetResult();

            var result = new EventArrayStruct();
            if (groups == null || groups.Count == 0)
            {
                result.Events = IntPtr.Zero;
                result.Count = 0;

                IntPtr emptyResultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
                Marshal.StructureToPtr(result, emptyResultPtr, false);
                return emptyResultPtr;
            }

            result.Events = Marshal.AllocHGlobal(Marshal.SizeOf<BotGroupStruct>() * groups.Count);
            result.Count = groups.Count;
            for (int i = 0; i < groups.Count; i++)
            {
                Marshal.StructureToPtr(
                    (BotGroupStruct)groups[i],
                    result.Events + i * Marshal.SizeOf<BotGroupStruct>(),
                    false
                );
            }

            IntPtr resultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
            Marshal.StructureToPtr(result, resultPtr, false);
            return resultPtr;
        }
        
        [UnmanagedCallersOnly(EntryPoint = "GetMemberList")]
        public static IntPtr GetMemberList(int index, long groupUin, bool refresh /*= false*/)
        {
            if (Program.Contexts.Count <= index)
            {
                return IntPtr.Zero;
            }

            var context = Program.Contexts[index].BotContext;
            var members = context.FetchMembers(groupUin, refresh).GetAwaiter().GetResult();

            var result = new EventArrayStruct();
            if (members == null || members.Count == 0)
            {
                result.Events = IntPtr.Zero;
                result.Count = 0;

                IntPtr emptyResultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
                Marshal.StructureToPtr(result, emptyResultPtr, false);
                return emptyResultPtr;
            }

            result.Events = Marshal.AllocHGlobal(Marshal.SizeOf<BotGroupMemberStruct>() * members.Count);
            result.Count = members.Count;
            for (int i = 0; i < members.Count; i++)
            {
                Marshal.StructureToPtr(
                    (BotGroupMemberStruct)members[i],
                    result.Events + i * Marshal.SizeOf<BotGroupStruct>(),
                    false
                );
            }

            IntPtr resultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
            Marshal.StructureToPtr(result, resultPtr, false);
            return resultPtr;
        }

        [UnmanagedCallersOnly(EntryPoint = "FetchGroupNotifications")]
        public static IntPtr FetchGroupNotifications(int index, ulong count, ulong start /*= 0*/)
        {
            if (Program.Contexts.Count <= index)
            {
                return IntPtr.Zero;
            }

            var context = Program.Contexts[index].BotContext;
            var notifications = context.FetchGroupNotifications(count, start).GetAwaiter().GetResult();

            return GetGroupNotificationsStructPtr(notifications);
        }

        [UnmanagedCallersOnly(EntryPoint = "FetchFilteredGroupNotifications")]
        public static IntPtr FetchFilteredGroupNotifications(int index, ulong count, ulong start /*= 0*/)
        {
            if (Program.Contexts.Count <= index)
            {
                return IntPtr.Zero;
            }

            var context = Program.Contexts[index].BotContext;
            var notifications = context.FetchFilteredGroupNotifications(count, start).GetAwaiter().GetResult();

            return GetGroupNotificationsStructPtr(notifications);
        }

        [UnmanagedCallersOnly(EntryPoint = "SetGroupNotification")]
        public static void SetGroupNotification(int index, long groupUin, ulong sequence, int type, bool isFiltered, int operate, ByteArrayNative message)
        {
            if (Program.Contexts.Count <= index)
            {
                return;
            }

            var context = Program.Contexts[index].BotContext;
            if (message.IsEmpty())
            {
                context.SetGroupNotification(groupUin, sequence, (BotGroupNotificationType)type, isFiltered, (GroupNotificationOperate)operate);
                return;
            }

            context.SetGroupNotification(groupUin, sequence, (BotGroupNotificationType)type, isFiltered, (GroupNotificationOperate)operate, Encoding.UTF8.GetString(message.ToByteArrayWithoutFree()));
        }

        [UnmanagedCallersOnly(EntryPoint = "SetGroupReaction")]
        public static void SetGroupReaction(int index, long groupUin, ulong sequence, ByteArrayNative code, bool isAdd)
        {
            if (Program.Contexts.Count <= index)
            {
                return;
            }

            var context = Program.Contexts[index].BotContext;
            context.SetGroupReaction(groupUin, sequence, Encoding.UTF8.GetString(code.ToByteArrayWithoutFree()), isAdd);
        }

        private static IntPtr GetGroupNotificationsStructPtr(List<BotGroupNotificationBase> notifications)
        {
            EventArrayStruct result = new EventArrayStruct();
            if (notifications == null || notifications.Count == 0)
            {
                result.Events = IntPtr.Zero;
                result.Count = 0;

                IntPtr emptyResultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
                Marshal.StructureToPtr(result, emptyResultPtr, false);
                return emptyResultPtr;
            }

            int addressLength = 0;
            for (int i = 0; i < notifications.Count; i++)
            {
                addressLength += notifications[i].Type switch
                {
                    BotGroupNotificationType.Join => Marshal.SizeOf<BotGroupJoinNotificationStruct>(),
                    BotGroupNotificationType.SetAdmin => Marshal.SizeOf<BotGroupSetAdminNotificationStruct>(),
                    BotGroupNotificationType.KickOther => Marshal.SizeOf<BotGroupKickOtherNotificationStruct>(),
                    BotGroupNotificationType.KickSelf => Marshal.SizeOf<BotGroupKickSelfNotificationStruct>(),
                    BotGroupNotificationType.Exit => Marshal.SizeOf<BotGroupExitNotificationStruct>(),
                    BotGroupNotificationType.UnsetAdmin => Marshal.SizeOf<BotGroupUnsetAdminNotificationStruct>(),
                    BotGroupNotificationType.Invite => Marshal.SizeOf<BotGroupInviteNotificationStruct>(),
                    _ => throw new ArgumentOutOfRangeException($"Out of notification of type: {notifications[i].Type}"),
                };
            }

            result.Events = Marshal.AllocHGlobal(addressLength);
            result.Count = notifications.Count;
            for (int i = 0; i < notifications.Count; i++)
            {
                switch (notifications[i].Type)
                {
                    case BotGroupNotificationType.Join:
                        Marshal.StructureToPtr(
                            (BotGroupJoinNotificationStruct)(BotGroupJoinNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupJoinNotificationEventStruct>(),
                            false
                        );
                        break;
                    case BotGroupNotificationType.SetAdmin:
                        Marshal.StructureToPtr(
                            (BotGroupSetAdminNotificationStruct)(BotGroupSetAdminNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupSetAdminNotificationStruct>(),
                            false
                        );
                        break;
                    case BotGroupNotificationType.KickOther:
                        Marshal.StructureToPtr(
                            (BotGroupKickOtherNotificationStruct)(BotGroupKickNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupKickOtherNotificationStruct>(),
                            false
                        );
                        break;
                    case BotGroupNotificationType.KickSelf:
                        Marshal.StructureToPtr(
                            (BotGroupKickSelfNotificationStruct)(BotGroupKickNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupKickSelfNotificationStruct>(),
                            false
                        );
                        break;
                    case BotGroupNotificationType.Exit:
                        Marshal.StructureToPtr(
                            (BotGroupExitNotificationStruct)(BotGroupExitNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupExitNotificationStruct>(),
                            false
                        );
                        break;
                    case BotGroupNotificationType.UnsetAdmin:
                        Marshal.StructureToPtr(
                            (BotGroupUnsetAdminNotificationStruct)(BotGroupUnsetAdminNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupUnsetAdminNotificationStruct>(),
                            false
                        );
                        break;
                    case BotGroupNotificationType.Invite:
                        Marshal.StructureToPtr(
                            (BotGroupInviteNotificationStruct)(BotGroupInviteNotification)notifications[i],
                            result.Events + i * Marshal.SizeOf<BotGroupInviteNotificationStruct>(),
                            false
                        );
                        break;
                }
            }

            var resultPtr = Marshal.AllocHGlobal(Marshal.SizeOf<EventArrayStruct>());
            Marshal.StructureToPtr(result, resultPtr, false);
            return resultPtr;
        }
    }
}