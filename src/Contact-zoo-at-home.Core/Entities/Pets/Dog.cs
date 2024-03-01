using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    public enum DogSubSpecies
    {
        Ordinary_Dog,
        Extraordinary_Dog
    }
    public class Dog : BasePet
    {
        public DogSubSpecies SubSpecies { get; set; }
    }
}
