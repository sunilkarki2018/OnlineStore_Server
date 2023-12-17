using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Database;

namespace Ecommerce.WebAPI.src.Repository
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
