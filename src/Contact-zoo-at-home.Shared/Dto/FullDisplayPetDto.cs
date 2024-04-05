using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class FullDisplayPetDto
    {
        public int Id { get; set; }
        public byte[][] Images { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public float Rating { get; set; }
        public string Breed { get; set; }
        public string ShortDescription { get; set; }
        public string Description {  get; set; }
        public IEnumerable<PetCommentsDto> Comments { get; set; }
    }
}
