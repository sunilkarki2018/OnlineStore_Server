using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Services
{
    public class OrderService : BaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IUserService _userService;
        public OrderService(IOrderRepo repo, IMapper mapper, IUserService userService) : base(repo, mapper)
        {
            _mapper = mapper;
            _orderRepo = repo;
            _userService = userService;
        }

        public async Task<OrderReadDTO> CreateOrderAsync(Guid userId, OrderCreateDTO orderCreateDTO)
        {
            var order = _mapper.Map<OrderCreateDTO, Order>(orderCreateDTO);
            order.UserId = userId;
            order.OrderNumber = GenerateRandomNumber().ToString();
            var createdOrder = await _orderRepo.CreateOneAsync(order);
            var createdOrderReadDTO = _mapper.Map<Order, OrderReadDTO>(createdOrder);
            createdOrderReadDTO.User = await _userService.GetByIdAsync(userId);
            return createdOrderReadDTO;
        }
        static int GenerateRandomNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public override async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDTO updateObject)
        {
            var existingOrder = await _orderRepo.GetByIdAsync(id);
            if (existingOrder is null)
            {
                throw CustomException.NotFoundException("Order doesnot exist");
            }
            _mapper.Map<OrderUpdateDTO, Order>(updateObject, existingOrder);
            return await _repo.UpdateOneAsync(existingOrder);
        }
    }
}
