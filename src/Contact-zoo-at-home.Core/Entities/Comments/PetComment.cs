using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Comments
{
    public class PetComment : BaseComment
    {
        public BasePet CommentTarget { get; set; }
        public PetComment? AnswerTo { get; set; }
    }
}
