#if UNITY_ANDROID
using Notifications.Android;
#elif UNITY_IOS
using Notifications.iOS;
#endif


namespace Notifications
{
    public abstract class NativeNotificationManager
    {
        public abstract string ScheduleNotification(NotificationData notification);
        public abstract void CancelNotification(string id);
        public abstract NotificationData? TryGetLastNotification();
    }
}