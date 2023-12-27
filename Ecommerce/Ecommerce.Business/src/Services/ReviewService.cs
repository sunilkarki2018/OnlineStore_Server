using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Services
{
    public class ReviewService : BaseService<Review, ReviewReadDTO, ReviewCreateDTO, ReviewUpdateDTO>, IReviewService
    {
        protected readonly IReviewRepo _reviewRepo;
        public ReviewService(IReviewRepo reviewRepo, IMapper mapper) : base(reviewRepo, mapper)
        {
            _reviewRepo = reviewRepo;
        }
        public async Task<ReviewReadDTO> CreateReviewAsync(Guid userId, ReviewCreateDTO reviewCreateDTO)
        {
            var review = _mapper.Map<ReviewCreateDTO, Review>(reviewCreateDTO);
            review.UserId = userId;
            return _mapper.Map<Review, ReviewReadDTO>(await _reviewRepo.CreateOneAsync(review));
        }
    }
}
