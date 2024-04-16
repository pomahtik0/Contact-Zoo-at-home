using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto.Pet
{
    public class ExtraPetOptionsDTO
    {
        public int Id { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
    }
}
