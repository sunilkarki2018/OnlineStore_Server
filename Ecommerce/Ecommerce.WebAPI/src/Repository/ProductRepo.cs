using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public override async Task<IEnumerable<Product>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _data.Include(u => u.ProductLine).Include(u => u.ProductSize).AsNoTracking().Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }
        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _data.Include(u => u.ProductLine).Include(u => u.ProductSize).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
