using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperTest.Models
{
    public class ComplexPetDTO
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string ShortDescription {  get; set; }
        public string Description { get; set; }
    }
}
