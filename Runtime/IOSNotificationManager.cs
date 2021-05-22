#if UNITY_IOS
using System;
using Unity.Notifications.iOS;

namespace Notifications.iOS
{
    public class IOSNotificationManager : NativeNotificationManager
    {
        public override void CancelNotification(string id)
        {
            iOSNotificationCenter.RemoveScheduledNotification(id);
        }

        public override string ScheduleNotification(NotificationData notification)
        {
            iOSNotificationCenter.ScheduleNotification(notification);
            return notification.Reason.ToString();
        }

        public override NotificationData? TryGetLastNotification()
        {
            return iOSNotificationCenter.GetLastRespondedNotification();
        }
    }
}


namespace Notifications
{
    public partial struct NotificationData
    {
        public static implicit operator iOSNotification(NotificationData data)
        {
            return new iOSNotification
            {
                Trigger = new iOSNotificationCalendarTrigger
                {
                    Day = data.FireTime.Day,
                    Hour = data.FireTime.Hour,
                    Minute = data.FireTime.Minute,
                    Month = data.FireTime.Month,
                    Second = data.FireTime.Second,
                    Year = data.FireTime.Year,
                    Repeats = false,
                },

                Title = data.Title,
                Subtitle = data.Subtitle,
                Body = data.Text,
                ShowInForeground = true,
                ThreadIdentifier = "thread1",
                CategoryIdentifier = "category_a",
                Identifier = data.Reason.ToString(),
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,

            };
        }

        public static implicit operator NotificationData?(iOSNotification notification)
        {
            if (notification == null)
                return null;

            return new NotificationData
            {
                Text = notification.Body,
                FireTime = DateTime.UtcNow,
                Reason = (NotificationReason)Enum.Parse(typeof(NotificationReason), notification.Identifier),
                Title = notification.Title,
                Subtitle = notification.Subtitle,
            };
        }
    }
}
#endif