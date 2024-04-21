using Contact_zoo_at_home.Application.Interfaces.Admin;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Realizations.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CombineSpeciesAsync(int mainId,
            [MaxLength(5)] IList<int> idsToCombine)
        {
            idsToCombine.Remove(mainId);

            if(idsToCombine.IsNullOrEmpty())
            {
                return;
            }

            await _dbContext.Pets
                .Where(pet => idsToCombine.
                Contains(pet.Species.Id))
                .ExecuteUpdateAsync(pet => 
                pet.SetProperty(x => x.Species, 
                                new Core.Entities.Pets.PetSpecies { Id = mainId }));

            await _dbContext.PetSpecies
                    .Where(species => idsToCombine.Contains(species.Id))
                    .ExecuteDeleteAsync();
        }
    }
}
