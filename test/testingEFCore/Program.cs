using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace testingEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Write
            CustomerUser customerUser = new CustomerUser()
            {
                Id = 1,
                FullName = "test Customer",
                UserName = "TestCustomer",
                ContactEmail = "SUPERCUSTOMER@HI.UA"
            };
            IndividualPetOwner individualPetOwner = new IndividualPetOwner()
            {
                Id = 2,
                FullName = "I Own Pets",
                UserName = "PetOwner228",
                ContactPhone = "12312-3123-2"
            };
            Pet Dog = new Pet()
            {
                Color = "Green",
                Name = "Jack",
                Species = "Dog",
                SubSpecies = "Haski",
                ActivityType = Contact_zoo_at_home.Core.Enums.PetActivityType.Any
            };

            individualPetOwner.OwnedPets.Add(Dog);
            Dog.Owner = individualPetOwner;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Add(customerUser);
                context.Add(individualPetOwner);
                context.SaveChanges();
            }
            #endregion
            #region Read
            Pet pet;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                pet = context.Pets.Include(x => x.Owner).First();
            }

            Console.WriteLine($"{pet.Name} {pet.Owner.FullName}\n{pet.Owner.UserName}");
            #endregion
            #region Update
            #endregion
        }
    }
}
