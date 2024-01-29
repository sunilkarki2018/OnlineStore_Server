using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class ProductLineRepo : BaseRepo<ProductLine>, IProductLineRepo
    {
        protected readonly DbSet<Core.src.Entities.Image> _images;
        public ProductLineRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
            _images = _databaseContext.Set<Core.src.Entities.Image>(); ;
        }

        public override async Task<IEnumerable<ProductLine>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _data.Include(u => u.Images).AsNoTracking().Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }

        public override async Task<ProductLine?> GetByIdAsync(Guid id)
        {
            return await _data.Include(u => u.Images).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateProductLineWithImagesAsync(ProductLine updateObject)
        {
            using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await _data.AsNoTracking().FirstOrDefaultAsync(e => e.Id == updateObject.Id) is null)
                    {
                        return false;
                    }
                    var imageList = await _images.AsNoTracking().Where(u => u.ProductLineId == updateObject.Id).AsNoTracking().ToListAsync();

                    if (imageList.Count > 0)
                    {
                        for (int i = 0; i < imageList.Count; i++)
                        {
                            _images.Remove(imageList[i]);
                        }
                    }

                    _data.Update(updateObject);
                    await _databaseContext.SaveChangesAsync();
                    _databaseContext.Entry(updateObject).State = EntityState.Detached;

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}