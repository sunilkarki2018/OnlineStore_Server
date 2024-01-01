using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        protected readonly IProductRepo _productRepo;
        protected readonly DbSet<Order> _orders;
        protected readonly DbSet<OrderItem> _orderItems;
        public OrderRepo(DatabaseContext databaseContext, IProductRepo productRepo) : base(databaseContext)
        {
            _productRepo = productRepo;
            _orders = _databaseContext.Set<Order>();
            _orderItems = _databaseContext.Set<OrderItem>();
        }

        public override async Task<Order> CreateOneAsync(Order createObject)
        {
            using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var item in createObject.orderItems)
                    {
                        var product = await _productRepo.GetByIdAsync(item.ProductId);
                        if (product == null)
                            throw CustomException.NotFoundException("Product not avaialbe to order");
                        if (product.Inventory < item.Quantity)
                            throw CustomException.ProductNotAvailableException("Enough stock not avaialve to order");
                        product.Inventory = product.Inventory - item.Quantity;
                        await _productRepo.UpdateOneAsync(product);

                        _databaseContext.Entry(product).State = EntityState.Detached;
                        if (product.ProductSize != null)
                        {
                            _databaseContext.Entry(product.ProductSize).State = EntityState.Detached;
                        }
                        if (product.ProductLine != null)
                        {
                            _databaseContext.Entry(product.ProductLine).State = EntityState.Detached;
                        }
                    }
                    await base.CreateOneAsync(createObject);
                    await _databaseContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error while processing order");
                }
            }
            return createObject;
        }

        public override async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orders.Include(u => u.orderItems).ThenInclude(v => v.Product).ThenInclude(w => w.ProductLine).Include(v => v.User).AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
        public override async Task<IEnumerable<Order>> GetAllAsync(GetAllOptions getAllOptions)
        {
            return await _data.Include(u => u.orderItems).Include(v => v.User).AsNoTracking().Skip(getAllOptions.Offset).Take(getAllOptions.Limit).ToArrayAsync();
        }

        public override async Task<bool> DeleteOneAsync(Order deleteObject)
        {
            if (await _data.AsNoTracking().FirstOrDefaultAsync(e => e.Id == deleteObject.Id) is null)
            {
                return false;
            }
            _orders.FromSqlRaw("Delete from table Orders where Id=", deleteObject.Id);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
