using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto.Notifications
{
    public class SimplifiedNotificationDto
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public int? NotificationTargetId { get; set; }
    }
}
