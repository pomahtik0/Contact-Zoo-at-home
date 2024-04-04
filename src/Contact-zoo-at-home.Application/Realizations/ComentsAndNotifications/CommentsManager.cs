using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications
{
    public class CommentsManager : BaseService
    {
        public CommentsManager() : base()
        {

        }

        public CommentsManager(DbConnection activeDbConnection) : base(activeDbConnection)
        {

        }

        public CommentsManager(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public CommentsManager(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

        }

        private float UpdateRating(float oldRating, int numberOfVoters, float newRatingMark)
        {
            oldRating = (oldRating * numberOfVoters + newRatingMark) / (numberOfVoters + 1);
            return oldRating;
        }

        public async Task LeaveCommentForUserAsync(PetComment petComment)
        {
            // validation checks

            petComment.Date = DateTime.Now;

            _dbContext.Attach(petComment);

            await _dbContext.SaveChangesAsync();
        }

        public async Task LeaveCommentForPetAsync(UserComment userComment)
        {
            // validation checks

            if (userComment.CommentRating != 0)
            {
                var user = _dbContext.Users.Find(userComment.CommentTarget.Id) ?? throw new Exception();
                user.CurrentRating = UpdateRating(user.CurrentRating, user.RatedBy, userComment.CommentRating);
                user.RatedBy++;
            }

            userComment.Date = DateTime.Now;

            _dbContext.Attach(userComment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
