#if UNITY_ANDROID
using Notifications.Android;
#elif UNITY_IOS
using Notifications.iOS;
#endif


namespace Notifications
{
    public enum NotificationReason
    {
        INVALID = 0,
        LIVES_RESTORED,
        FIRST_DAY,
        THIRD_DAY,
        SEVENTH_DAY,
        FREE_CHEST,
        FREE_BONUS_LEVEL,

    }
}