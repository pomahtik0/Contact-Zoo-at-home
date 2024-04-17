using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Basics.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Translations.Infrastructure.Entities
{
    public class PetSpeciesTranslative
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
    }
}
