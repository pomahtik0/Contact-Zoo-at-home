using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto.Notifications
{
    public class NotificationDto : SimplifiedNotificationDto
    {
        public string Text { get; set; }
        public int? RateTargetPetId { get; set; }
        public int? RateTargetUserId { get; set; }
    }
}
