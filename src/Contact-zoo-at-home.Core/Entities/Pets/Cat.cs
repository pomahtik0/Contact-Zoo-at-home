using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    public enum CatSubSpecies
    {
        British_Shorthair,
        Egyptian_Mau,
        Maine_Coon,
        Persian
    }
    public class Cat : BasePet
    {
        public CatSubSpecies SubSpecies { get; set; } // string mb
    }
}
