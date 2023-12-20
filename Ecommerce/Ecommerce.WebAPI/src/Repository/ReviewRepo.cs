using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Database;

namespace Ecommerce.WebAPI.src.Repository
{
    public class ReviewRepo : BaseRepo<Review>, IReviewRepo
    {
        public ReviewRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
