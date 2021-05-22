#if UNITY_ANDROID
using System;
using Unity.Notifications.Android;
using UnityEngine;

namespace Notifications.Android
{
    public class AndroidNotificationManager : NativeNotificationManager
    {
        public const string _baseChannelName = "SKNC";

        public AndroidNotificationManager()
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = _baseChannelName,
                Name = "Swipe knight",
                Importance = Importance.High,
                Description = "Swipe knight notification channel",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        public override void CancelNotification(string id)
        {
            var intID = int.Parse(id);
            AndroidNotificationCenter.CancelNotification(intID);
        }

        public override string ScheduleNotification(NotificationData notification)
        {
            return AndroidNotificationCenter.SendNotification(notification, "channel_id").ToString();
        }

        public override NotificationData? TryGetLastNotification()
        {
            return AndroidNotificationCenter.GetLastNotificationIntent();
        }
    }
}

namespace Notifications
{
    public partial struct NotificationData
    {
        public static implicit operator AndroidNotification(NotificationData data)
        {
            return new AndroidNotification
            {
                FireTime = data.FireTime,
                Title = data.Title,
                Text = data.Text,
                SmallIcon = data.SmallIcon,
                LargeIcon = data.LargeIcon,
                UsesStopwatch = false,
                Group = Application.productName,
                GroupAlertBehaviour = GroupAlertBehaviours.GroupAlertAll,
                ShouldAutoCancel = false,
                IntentData = data.Reason.ToString(),
            };
        }

        public static implicit operator NotificationData?(AndroidNotificationIntentData data)
        {
            if (data == null)
                return null;

            return new NotificationData
            {
                Title = data.Notification.Title,
                Text = data.Notification.Text,
                FireTime = data.Notification.FireTime,
                SmallIcon = data.Notification.SmallIcon,
                LargeIcon = data.Notification.LargeIcon,
                Reason = (NotificationReason)Enum.Parse(typeof(NotificationReason), data.Notification.IntentData),
            };
        }
    }
}
#endif