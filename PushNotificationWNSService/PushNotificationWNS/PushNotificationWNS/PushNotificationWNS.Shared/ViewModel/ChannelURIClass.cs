using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;

namespace PushNotificationWNS.ViewModel
{
    /// <summary>
    /// Request ChannelURI for application, used to authenticate with WNS
    /// </summary>

    /// <summary>
    /// Sending ChannelURI to Server is in process, update it once I complete. 
    /// As of now, copy ChannelURI you received and use it in your server to check Notifications.
    /// Source : http://msdn.microsoft.com/en-in/library/windows/apps/hh868221.aspx for sending ChannelURI to server.
    /// </summary>    
    
    public class ChannelURIClass
    {
        PushNotificationChannel channel = null;
        /// <summary>
        /// Constructor.
        /// </summary>
        public ChannelURIClass()
        {
            ChannelURI();
            Debug.WriteLine(Environment.NewLine + "Channel URI : " + channel.Uri);
        }

        /// <summary>
        /// ChannelURI function.
        /// </summary>
        public async void ChannelURI()
        {
            try
            {
                /// <summary>
                /// Requesting channel URI for Primary Tile. Request SecondaryAsync for Secondary Tile.
                /// </summary>
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                /// <summary>
                /// Push Notification received event initialization.
                /// </summary>
                channel.PushNotificationReceived += channel_PushNotificationReceived;
            }

            catch (Exception ex)
            {
                /// <summary>
                /// Could not create a channel.
                /// </summary>
                Debug.WriteLine(ex.Message);                
            }
        }

        /// <summary>
        /// Invokes when push notification is received.
        /// </summary>
        void channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            String notificationContent = String.Empty;

            switch (e.NotificationType)
            {
                case PushNotificationType.Badge:
                    notificationContent = e.BadgeNotification.Content.GetXml();
                    break;

                case PushNotificationType.Tile:
                    notificationContent = e.TileNotification.Content.GetXml();
                    break;

                case PushNotificationType.Toast:
                    notificationContent = e.ToastNotification.Content.GetXml();
                    break;

                case PushNotificationType.Raw:
                    notificationContent = e.RawNotification.Content;
                    break;
            }
            /// <summary>
            /// Getting notificationContent and converting it into XML document.
            /// </summary>
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(notificationContent);

            /// <summary>
            /// ToastNotification, creates and display toast notification.
            /// </summary>
            ToastNotification toast = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

            /// <summary>
            /// BadgeNotification, creates and update badge notification
            /// </summary>
            BadgeNotification badge = new BadgeNotification(xml);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);

            /// <summary>
            /// TileNotification, creates and update tile notification
            /// </summary>
            TileNotification tileNotification = new TileNotification(xml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);

            /// <summary>
            /// Cancel the event by setting it True.
            /// </summary>
            e.Cancel = true;
        }
    }
}
