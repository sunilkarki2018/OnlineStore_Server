using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Controller.src
{
    public class ReviewController : BaseController<Review, ReviewReadDTO, ReviewCreateDTO, ReviewUpdateDTO>
    {
        public ReviewController(IReviewService service) : base(service)
        {
        }
    }
}
