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
            //return await _data.Include(u => u.ProductLine).Include(u => u.ProductSize).AsNoTracking().Where(s => s.ProductLine.Title.Contains(getAllOptions.Search) && s.ProductLine.CategoryId == getAllOptions.CategoryId).Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();

            //var query = _data.Include(u => u.ProductLine).Include(u => u.ProductSize);
            /*
              if (!string.IsNullOrEmpty(getAllOptions.Search))
              {
                  query.Where(s => s.ProductLine.Title.Contains(getAllOptions.Search!));
              }
              if (!string.IsNullOrEmpty(getAllOptions.CategoryId.ToString()))
              {
                  query.Where(s => s.ProductLine.CategoryId == getAllOptions.CategoryId!);
              }
            */
            // return await query.Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();

            return await _data.Include(u => u.ProductLine).Include(u => u.ProductSize).AsNoTracking().ToArrayAsync();
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
