using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Database;

namespace Ecommerce.WebAPI.src.Repository
{
    public class ProductSizeRepo: BaseRepo<ProductSize>, IProductSizeRepo
    {   public ProductSizeRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        
    }
}