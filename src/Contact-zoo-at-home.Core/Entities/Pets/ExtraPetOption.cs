using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// For extra pet information, needed by users.
    /// For example Weight of Fur, or length of teeth, or is it poison?
    /// </summary>
    public class ExtraPetOption
    {
        public int Id { get; set; }
        public string OptionName { get; set; } = string.Empty;
        public string OptionValue { get; set; } = string.Empty;
        public Language OptionLanguage { get; set; }
    }
}
