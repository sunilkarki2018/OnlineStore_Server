using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebAPI.src.Repository
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        protected readonly IProductRepo _productRepo;
        protected readonly DbSet<OrderItem> _data;
        public OrderRepo(DatabaseContext databaseContext, IProductRepo productRepo) : base(databaseContext)
        {
            _productRepo = productRepo;
            _data = _databaseContext.Set<OrderItem>();
        }
        public override async Task<Order> CreateOneAsync(Order createObject)
        {
            using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await base.CreateOneAsync(createObject);
                    foreach (var item in createObject.orderItems)
                    {
                        var product = await _productRepo.GetByIdAsync(item.ProductId);
                        if (product == null)
                            throw CustomException.NotFoundException("Product not avaialbe to order");
                        if (product.Quantity < item.Quantity)
                            throw CustomException.ProductNotAvailableException("Enough stock not avaialve to order");
                        product.Quantity = product.Quantity - item.Quantity;
                        await _productRepo.UpdateOneAsync(product);
                        item.OrderId = createObject.Id;
                        await _data.AddAsync(item);
                    }
                    await _databaseContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return createObject;
        }
    }
}
