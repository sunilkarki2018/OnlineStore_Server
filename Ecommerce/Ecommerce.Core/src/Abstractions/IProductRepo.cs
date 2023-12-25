using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Abstractions
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        Task<int> GetProductsRecordCountAsync(GetAllOptions getAllOptions);
    }
}