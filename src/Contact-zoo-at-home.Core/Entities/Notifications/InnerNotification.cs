﻿using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Notifications
{
    /// <summary>
    /// Notification that should be shown on login.
    /// </summary>
    public class InnerNotification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public NotificationStatus Status { get; set; }
        public BaseUser NotificationTarget {  get; set; }
    }
}
