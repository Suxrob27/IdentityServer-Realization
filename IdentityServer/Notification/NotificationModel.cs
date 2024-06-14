using Microsoft.Data.SqlClient;

namespace IdentityServer.Notification
{
    public class NotificationModel
    {
        public string Property { get; set; }

        public NotificationType notificationType { get; set;}
    }
}
