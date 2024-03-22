using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class PetOwnerManager : IDisposable
    {
        private bool _disposeConnection;
        private DbConnection _connection;
        private DbTransaction? _transaction;

        public PetOwnerManager(DbConnection? activeDbConnection = null)
        {
            if (activeDbConnection == null)
            {
                _disposeConnection = true;
            }

            _connection = activeDbConnection ?? DBConnections.GetNewDbConnection();
        }

        public PetOwnerManager(DbTransaction activeDbTransaction)
        {
            if (activeDbTransaction?.Connection is null)
            {
                throw new ArgumentNullException("Transaction is null, or it's connection has closed");
            }

            _connection = activeDbTransaction.Connection;
            _transaction = activeDbTransaction;
        }

        public void Dispose()
        {
            if (_disposeConnection)
            {
                _connection.Dispose(); // Ensure connection will be disposed, it is not managed somewhere else.
            }
        }

        public async Task<IList<Pet>> GetAllOwnedPetsAsync(int ownerId)
        {
            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                dbContext.Database.UseTransaction(_transaction);
            }

            var pets = await dbContext.Pets.Where(pet => pet.Owner.Id == ownerId)
                .AsNoTracking()
                .Include(x => x.Species)
                .ThenInclude(x => x.Names)
                .AsSplitQuery()
                .Include(x => x.Breed)
                .ThenInclude(x => x.Names)
                .ToListAsync();

            return pets;
        }

        public async Task<Pet> GetOwnedPetAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                dbContext.Database.UseTransaction(_transaction);
            }

            var pet = await dbContext.Pets.Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.Species)
                .ThenInclude(species => species.Names)
                .AsSplitQuery()
                .Include(pet => pet.Breed)
                .ThenInclude(breed => breed.Names)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new ArgumentException($"User with specified Id={ownerId} is not an owner of a pet", nameof(ownerId));
            }

            return pet;
        }

        public async Task<Pet> GetOwnedPetImagesAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                dbContext.Database.UseTransaction(_transaction);
            }

            var pet = await dbContext.Pets.Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.Images)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new ArgumentException($"User with specified Id={ownerId} is not an owner of a pet", nameof(ownerId));
            }

            return pet;
        }

        public async Task<Pet> GetOwnedPetBlockedDatesAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                dbContext.Database.UseTransaction(_transaction);
            }

            var pet = await dbContext.Pets.Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.BlockedDates)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new ArgumentException($"User with specified Id={ownerId} is not an owner of a pet", nameof(ownerId));
            }

            return pet;
        }

        public async Task CreateNewOwnedPetAsync(Pet newPet, int ownerId)
        {
            if (newPet is null)
            {
                throw new ArgumentNullException("No pet to create is specified");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                dbContext.Database.UseTransaction(_transaction);
            }

            var owner = await dbContext.PetOwners.FindAsync(ownerId);

            if (owner == null)
            {
                throw new ArgumentException($"No pet owner, with Id={ownerId} exists.", nameof(ownerId));
            }

            dbContext.Attach(newPet);

            newPet.Owner = owner;

            if (newPet.PetOptions != null)
            {
                dbContext.Attach(newPet.PetOptions);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
