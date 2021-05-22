using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Notifications.Android;
#elif UNITY_IOS
using Notifications.iOS;
#endif

using Unity.Notifications.Android;
using UnityEngine;
using System.Threading.Tasks;

namespace Notifications
{
    public interface INotificationService
    {
        void ScheduleNotification(NotificationData data);
    }

    public class NotificationService : INotificationService
    {
        private NativeNotificationManager _manager;

        public async Task Initialize()
        {
#if UNITY_ANDROID
            _manager = new AndroidNotificationManager();
#elif UNITY_IOS
            _manager = new IOSNotificationManager();
#endif

            var notification = _manager.TryGetLastNotification();

            if (notification.HasValue)
            {
                Debug.Log($"Was opened from notification: {notification}");
            }
        }

        public void ScheduleNotification(NotificationData data)
        {
            string reasonString = data.Reason.ToString();
            string id = PlayerPrefs.GetString(reasonString, "");
            bool wasScheduled = !string.IsNullOrEmpty(id);

            if (wasScheduled)
            {
                _manager.CancelNotification(id);
                PlayerPrefs.DeleteKey(reasonString);
            }

            var rescheduledID = _manager.ScheduleNotification(data);

            if (!string.IsNullOrEmpty(rescheduledID))
                PlayerPrefs.SetString(reasonString, rescheduledID);
        }
    }
}