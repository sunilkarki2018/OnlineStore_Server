using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IReviewService : IBaseService<Review, ReviewReadDTO, ReviewCreateDTO, ReviewUpdateDTO>
    {
    }
}
