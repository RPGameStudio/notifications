using System;
#if UNITY_ANDROID
using Notifications.Android;
#elif UNITY_IOS
using Notifications.iOS;
#endif


namespace Notifications
{
    public partial struct NotificationData
    {
        //base
        public string Title;
        public string Text;
        public NotificationReason Reason;
        public DateTime FireTime;

        //ios specific
        public string Subtitle; //for IOS apps

        //android specific
        public string SmallIcon;
        public string LargeIcon;


        public override string ToString()
        {
            return base.ToString() + $"\n{Reason}\n{Title}\n{Text}";
        }
    }
}