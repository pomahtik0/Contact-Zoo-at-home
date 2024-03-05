using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    public class PetBlockedDate
    {
        public DateTime BlockedDate { get; set; }
        public BlockedDateReason Reason { get; set; }
    }
}
