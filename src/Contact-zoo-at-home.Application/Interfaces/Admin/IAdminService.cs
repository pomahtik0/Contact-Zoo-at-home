using System.ComponentModel.DataAnnotations;

namespace Contact_zoo_at_home.Application.Interfaces.Admin
{
    public interface IAdminService
    {
        Task CombineSpeciesAsync(int mainId, [MaxLength(5)] IList<int> idsToCombine);
    }
}
