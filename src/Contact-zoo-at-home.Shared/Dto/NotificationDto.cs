using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text {  get; set; }
        public int NotificationTargetId {  get; set; }
    }

    public class RatingNotificationDto : NotificationDto
    {
        public int? RateTargetPetId { get; set; }
        public int? RateTargetUserId { get; set; }
    }
}
