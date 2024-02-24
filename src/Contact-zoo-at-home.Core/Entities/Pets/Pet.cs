using Contact_zoo_at_home.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    public abstract class Pet
    {
        public int Id { get; set; }
        public IPetOwner? Owner { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Rating { get; set; }
        public int RaitingUserVotesCount {  get; set; }
        public double Price { get; set; }
        public string Species { get; set; } = string.Empty; // Replace with enum mb
    }
}
