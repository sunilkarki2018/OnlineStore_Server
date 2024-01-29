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
            var query = _data.Include(u => u.ProductLine).ThenInclude(v => v.Images).Include(u => u.ProductSize).AsNoTracking();
            if (!string.IsNullOrEmpty(getAllOptions.SearchKey))
            {
                query = query.Where(u => u.ProductLine.Title.ToLower().Contains(getAllOptions.SearchKey.ToLower()));
            }
            return await query.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToListAsync();
        }

        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _data.Include(u => u.ProductLine).Include(u => u.ProductSize).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> GetProductsRecordCountAsync(GetAllOptions getAllOptions)
        {
            return await _data.CountAsync();
        }
    }
}
