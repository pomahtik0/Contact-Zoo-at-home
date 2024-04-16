using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Shared.Dto.Users;

namespace Contact_zoo_at_home.Shared.Dto.Notifications
{
    public class PetCommentsDto
    {
        public int Id { get; set; }
        public LinkedUserDto Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
