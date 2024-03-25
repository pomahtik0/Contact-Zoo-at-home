using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class IndividualOwnerManager : PetOwnerManager
    {
        public IndividualOwnerManager(DbConnection? connection) : base (connection) { }
        public IndividualOwnerManager(DbTransaction transaction) : base (transaction) { }

        public async Task SaveUniqueSettings/*rename me later*/(IndividualOwner individualOwner)
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
