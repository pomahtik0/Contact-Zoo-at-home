using Contact_zoo_at_home.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Comments
{
    public class UserComment : BaseComment
    {
        public BaseUser CommentTarget { get; set; }
        public float CommentRating { get; set; }
    }
}
