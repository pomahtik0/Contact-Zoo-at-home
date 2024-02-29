using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;

namespace testingEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Write
            CustomerUser customerUser = new CustomerUser()
            {
                FullName = "test Customer",
                UserName = "TestCustomer",
                ContactEmail = "SUPERCUSTOMER@HI.UA"
            };
            IndividualPetOwner individualPetOwner = new IndividualPetOwner()
            {
                FullName = "I Own Pets",
                UserName = "PetOwner228",
                ContactPhone = "12312-3123-2"
            };

            Dog dog = new Dog()
            {
                Owner = individualPetOwner
            };

            individualPetOwner.OwnedPets.Append(dog);

            StandartContract standartContract = new StandartContract()
            {
                Contractor = individualPetOwner,
                Customer = customerUser,
                PetRepresentatives = new List<IPetRepresentative> { individualPetOwner },
                PetsInContract = new List<BasePet> { dog }
            };

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Attach(customerUser);
                context.Attach(individualPetOwner);
                context.Attach(dog);
                context.Attach(standartContract);
                context.SaveChanges();
            }
            #endregion
            #region Read
            #endregion
            #region Update
            #endregion
        }
    }
}
