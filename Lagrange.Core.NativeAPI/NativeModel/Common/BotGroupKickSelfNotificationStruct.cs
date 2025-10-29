using System.Runtime.InteropServices;
using System.Text;
using Lagrange.Core.Common.Entity;

namespace Lagrange.Core.NativeAPI.NativeModel.Common;
[StructLayout(LayoutKind.Sequential)]
public class BotGroupKickSelfNotificationStruct(BotGroupKickNotification notification) : BotGroupNotificationBaseStruct(notification)
{
    public long OperatorUin = notification.OperatorUin;

    public ByteArrayNative OperatorUid { get; } = Encoding.UTF8.GetBytes(notification.OperatorUid);

    public static implicit operator BotGroupKickSelfNotificationStruct(BotGroupKickNotification e)
    {
        return new BotGroupKickSelfNotificationStruct(e);
    }
}
