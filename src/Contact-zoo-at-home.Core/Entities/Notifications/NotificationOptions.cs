using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Notifications
{
    public class NotificationOptions // struct?
    {
        public string[] OtherPhones { get; set; } = []; // mb list, depends on DB
        public bool NotifyOnTelegram { get; set; }
        public bool NotifyOnPhone { get; set; } // Sms?
        public bool NotifyOnViber { get; set; }
    }
}
