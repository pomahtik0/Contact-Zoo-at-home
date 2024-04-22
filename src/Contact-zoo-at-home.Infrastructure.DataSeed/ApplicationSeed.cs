using Bogus;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.Images;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Contact_zoo_at_home.Core.Entities.Pets;

namespace Contact_zoo_at_home.Infrastructure.DataSeed
{
    internal class ApplicationSeed : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public class SeedingOptions
        {
            public int NumberOfUsers { get; set; } = 3;
            public int NumberOfPetsPerPetOwner { get; set; } = 3;
        }

        private SeedingOptions _options = new SeedingOptions();

        private List<StandartUser> _users;

        public ApplicationSeed(string connectionString) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _dbContext = new ApplicationDbContext(optionsBuilder.Options);    
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void GenerateUsers()
        {
            int userId = 2;

            var profileImageGenerator = new Faker<ProfileImage>()
                                .RuleFor(i => i.Image, f =>
                                {
                                    var imageUrl = f.Image.PicsumUrl();
                                    using (WebClient client = new WebClient())
                                    {
                                        try
                                        {
                                            byte[] imageBytes = client.DownloadData(imageUrl);
                                            return Convert.ToBase64String(imageBytes);
                                        }
                                        catch
                                        {
                                            return null;
                                        }
                                    }
                                });

            var customerGenerator = new Faker<CustomerUser>()
                                .RuleFor(u => u.Id, f => userId)
                                .RuleFor(u => u.Name, f => f.Name.FullName())
                                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                                .RuleFor(u => u.PhoneNumber, (f, u) => f.Phone.PhoneNumber("###-###-##-##"))
                                .RuleFor(u => u.NotificationOptions, f => new NotificationOptions())
                                .RuleFor(u => u.ProfileImage, (f, u) => profileImageGenerator.Generate());

            var petImageGenerator = new Faker<PetImage>()
                                .RuleFor(u => u.Name, f => f.Name.JobTitle())
                                .RuleFor(u => u.Image, f =>
                                {
                                    var imageUrl = f.Image.PicsumUrl();
                                    using (WebClient client = new WebClient())
                                    {
                                        try
                                        {
                                            byte[] imageBytes = client.DownloadData(imageUrl);
                                            return Convert.ToBase64String(imageBytes);
                                        }
                                        catch
                                        {
                                            return "";
                                        }
                                    }
                                });

            var petSpecies = new PetSpecies[]
            {
                new PetSpecies()
                {
                    Name = "Dog"
                },
                new PetSpecies()
                {
                    Name = "Cat"
                },
                new PetSpecies()
                {
                    Name = "Fish"
                }
            };

            var petGenerator = new Faker<Pet>()
                                .RuleFor(p => p.Name, f => f.Name.FirstName())
                                .RuleFor(p => p.Description, f => f.Lorem.Letter(512))
                                .RuleFor(p => p.ShortDescription, f => f.Lorem.Letter(200))
                                .RuleFor(p => p.Images, f => petImageGenerator.Generate(1).ToList())
                                .RuleFor(p => p.Species, f => f.Random.ArrayElement<PetSpecies>(petSpecies));

            var individualOwnerGenerator = new Faker<IndividualOwner>()
                                .RuleFor(u => u.Id, f => userId)
                                .RuleFor(u => u.Name, f => f.Name.FullName())
                                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                                .RuleFor(u => u.PhoneNumber, (f, u) => f.Phone.PhoneNumber("###-###-##-##"))
                                .RuleFor(u => u.NotificationOptions, f => new NotificationOptions())
                                .RuleFor(u => u.ProfileImage, f => profileImageGenerator.Generate())
                                .RuleFor(u => u.OwnedPets, f => petGenerator.Generate(_options.NumberOfPetsPerPetOwner).ToList());

            var companyUsersGenerator = new Faker<Company>()
                                .RuleFor(u => u.Id, f => userId)
                                .RuleFor(u => u.Name, f => f.Name.FullName())
                                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                                .RuleFor(u => u.PhoneNumber, (f, u) => f.Phone.PhoneNumber("###-###-##-##"))
                                .RuleFor(u => u.NotificationOptions, f => new NotificationOptions())
                                .RuleFor(u => u.ProfileImage, (f, u) => profileImageGenerator.Generate())
                                .RuleFor(u => u.OwnedPets, f => petGenerator.Generate(_options.NumberOfPetsPerPetOwner).ToList())
                                .RuleFor(u => u.Description, f => f.Lorem.Letter(810));

            _users = new List<StandartUser>();

            for (int i = 0; i < _options.NumberOfUsers; i++)
            {
                _users.Add(customerGenerator.Generate());
                userId++;
            }

            for (int i = 0; i < _options.NumberOfUsers; i++)
            {
                _users.Add(individualOwnerGenerator.Generate());
                userId++;
            }

            for (int i = 0; i < _options.NumberOfUsers; i++)
            {
                _users.Add(companyUsersGenerator.Generate());
                userId++;
            }
        }

        public void SeedData()
        {
            GenerateUsers();

            _dbContext.Database.EnsureCreated();
            _dbContext.AddRange(_users);
            _dbContext.SaveChanges();
        }
    }
}
