﻿using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application
{
    public static class PetManagement
    {
        public static async Task CreateNewPetAsync(Pet pet, int ownerId)
        {
            if (pet == null)
            {
                throw new ArgumentNullException();
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var petOwner = await dbContext.PetOwners.Where(user => user.Id == ownerId).FirstAsync();
                pet.Owner = petOwner;
                dbContext.Attach(pet);
                await dbContext.SaveChangesAsync();
            }
        }
        public static async Task<Pet> GetPetByIdAsync(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pet = await context.Pets.Where(pet => pet.Id == id).AsNoTracking().FirstAsync();
                return pet;
            }
        }

        public static async Task UpdatePetAsync(Pet pet)
        {
            if (pet == null)
            {
                throw new ArgumentNullException();
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                throw new NotImplementedException();
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<(IList<Pet> pets, int pages)> GetPetsAsync(int page)
        {
            page = page - 1;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pages = context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Count() / 25; // where 25, elements on the page
                var pets = await context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Skip(page * 25).Take(25).ToListAsync();
                return (pets, pages);
            }
        }
    }
}
