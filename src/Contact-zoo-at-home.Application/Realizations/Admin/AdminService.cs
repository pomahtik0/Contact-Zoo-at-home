using Contact_zoo_at_home.Application.Exceptions;
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
    internal class AdminService : IAdminService
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

            var pets = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => idsToCombine
                    .Contains(pet.Species.Id))
                .Include(pet => pet.Species)
                .ToListAsync();

            var species = await _dbContext.PetSpecies
                .Where(species => species.Id == mainId)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();


            foreach (var pet in pets)
            {
                pet.Species = species;
            }

            await _dbContext.SaveChangesAsync();

            await _dbContext.PetSpecies
                    .Where(species => idsToCombine.Contains(species.Id))
                    .ExecuteDeleteAsync();
        }
    }
}
