﻿using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    internal class IndividualOwnerManager : PetOwnerManager, IIndividualOwnerManager
    {
        public IndividualOwnerManager(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task SaveNewDescriptionAsync(IndividualOwner individualOwner)
        {
            if (individualOwner == null)
            {
                throw new ArgumentNullException (nameof (individualOwner));
            }

            if (individualOwner.Id <= 0)
            {
                throw new ArgumentOutOfRangeException (nameof(individualOwner.Id), $"Invalid Id={individualOwner.Id}");
            }

            await _dbContext.IndividualOwners.Where(owner => owner.Id == individualOwner.Id).ExecuteUpdateAsync(owner => owner.SetProperty(x => x.ShortDescription, individualOwner.ShortDescription));
        }
    }
}
