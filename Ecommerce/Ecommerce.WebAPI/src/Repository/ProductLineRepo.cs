using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class ProductLineRepo : BaseRepo<ProductLine>, IProductLineRepo
    {
        public ProductLineRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override async Task<IEnumerable<ProductLine>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _data.Include(u => u.Images).AsNoTracking().Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }

        public override async Task<ProductLine?> GetByIdAsync(Guid id)
        {
            return await _data.Include(u => u.Images).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        }
    }
}