using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Interfaces.OpenInfo
{
    public interface IPetInfo : IDisposable
    {
        Task<IList<Pet>> GetAllPetsAsync(int page, int numberOfPetsOnPage = 20);
        Task<Pet> GetPetProfileAsync(int petId);
        Task<IList<PetComment>> UploadMoreCommentsAsync(int petId, int lastCommentId);
    }
}
