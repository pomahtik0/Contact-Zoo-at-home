using Contact_zoo_at_home.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Comments
{
    public abstract class BaseComment
    {
        public int Id { get; set; }
        public BaseUser? Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
