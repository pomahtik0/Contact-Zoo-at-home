using Contact_zoo_at_home.Application.Interfaces.Admin;
using Contact_zoo_at_home.Translations;

namespace Contact_zoo_at_home.WebAPI.Extensions
{
    public static class AdminCombineSpeciesExtension
    {
        public static async Task CombineSpeciesWithTranslations(this IAdminService adminService, IAdminSpeciesTranslationService adminSpeciesTranslationService, int mainId, IList<int> ids)
        {
            await adminSpeciesTranslationService.Context.Database.BeginTransactionAsync();
            await adminSpeciesTranslationService.CombineSpeciesAsync(mainId, ids);
            try
            {
                await adminService.CombineSpeciesAsync(mainId, ids);
            }
            catch
            {
                await adminSpeciesTranslationService.Context.Database.RollbackTransactionAsync();
                throw;
            }
            await adminSpeciesTranslationService.Context.Database.CommitTransactionAsync();
        }
    }
}
