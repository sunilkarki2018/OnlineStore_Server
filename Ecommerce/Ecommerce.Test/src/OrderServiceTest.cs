using AutoMapper;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Services;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Moq;

namespace Ecommerce.Test.src
{
    public class OrderServiceTest
    {
        private readonly Mock<IOrderRepo> _mockRepo;
        private static IMapper _mapper;

        public OrderServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(m =>
                {
                    m.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void GetAllOrdersAsync_ShouldInvokeRepoOrdersMethod()
        {
            var repo = new Mock<IOrderRepo>();
            var userService = new Mock<IUserService>();
            var orderService = new OrderService(repo.Object, _mapper, userService.Object);
            GetAllOptions options = new GetAllOptions() { Limit = 5, Offset = 0 };

            await orderService.GetAllAsync(options);

            repo.Verify(repo => repo.GetAllAsync(options), Times.Once);
        }

        [Theory]
        [ClassData(typeof(GetAllOrdersData))]
        public async void GetAllOrdersAsync_ShouldReturnValidResponse(IEnumerable<Order> orderResponse, IEnumerable<OrderReadDTO> expected)
        {
            var orderRepo = new Mock<IOrderRepo>();
            var userService = new Mock<IUserService>();
            GetAllOptions options = new GetAllOptions() { Limit = 5, Offset = 0 };
            orderRepo.Setup(repo => repo.GetAllAsync(options)).ReturnsAsync(orderResponse);
            var orderService = new OrderService(orderRepo.Object, _mapper, userService.Object);

            var orderReadDTOresponse = await orderService.GetAllAsync(options);

            Assert.Equivalent(expected, orderReadDTOresponse);
        }

        public class GetAllOrdersData : TheoryData<IEnumerable<Order>, IEnumerable<OrderReadDTO>>
        {
            public GetAllOrdersData()
            {
                Order order1 = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Registered,
                    OrderNumber = "123456",
                    orderItems = new List<OrderItem>
                {
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
                };
                Order order2 = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Pending,
                    OrderNumber = "123456",
                    orderItems = new List<OrderItem>
                {
                    new OrderItem {
                        Price=20,
                        Quantity=100,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
                };
                Order order3 = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Registered,
                    OrderNumber = "123456",
                    orderItems = new List<OrderItem>
                {
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
                };
                IEnumerable<Order> orders = new List<Order>() { order1, order2, order3 };
                Add(orders, _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(orders));
            }
        }

        [Fact]
        public async void GetOrderByIdAsync_ShouldInvokeRepoCategoryMethod()
        {
            var id = Guid.NewGuid();
            var orderRepo = new Mock<IOrderRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new Mock<IUserService>();
            var orderService = new OrderService(orderRepo.Object, _mapper, userService.Object);

            await orderService.GetByIdAsync(id);

            orderRepo.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Theory]
        [ClassData(typeof(GetOrderByIdData))]
        public async void GetCategoryByIdAsync_ShouldReturnValidResponse(Order order, OrderReadDTO orderReadDTO, Type exceptionType)
        {
            var repo = new Mock<IOrderRepo>();
            repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(order);
            var userService = new Mock<IUserService>();
            var orderService = new OrderService(repo.Object, _mapper, userService.Object);
            var response = await orderService.GetByIdAsync(It.IsAny<Guid>());
            Assert.Equivalent(orderReadDTO, response);
        }

        public class GetOrderByIdData : TheoryData<Order?, OrderReadDTO?, Type?>
        {
            public GetOrderByIdData()
            {
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Registered,
                    OrderNumber = "123456",
                    orderItems = new List<OrderItem>
                {
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
                };
                OrderReadDTO orderReadDTO = _mapper.Map<Order, OrderReadDTO>(order);
                Add(order, orderReadDTO, null);
            }
        }

        [Fact]
        public async void CreateOrderAsync_ShouldInvokeRepoMethod()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var orderRepo = new Mock<IOrderRepo>();
            var mapper = new Mock<IMapper>();
            var userService = new Mock<IUserService>();

            var orderService = new OrderService(orderRepo.Object, _mapper, userService.Object);

            var orderCreateDTO = new OrderCreateDTO
            {
                OrderStatus = OrderStatus.Registered,
                OrderItems = new List<OrderItemCreateDTO>
                {
                    new OrderItemCreateDTO {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItemCreateDTO {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
            };
            orderRepo.Setup(r => r.CreateOneAsync(It.IsAny<Order>()))
      .ReturnsAsync(new Order());

            // Act
            await orderService.CreateOrderAsync(userId, orderCreateDTO);

            // Assert
            orderRepo.Verify(repo => repo.CreateOneAsync(It.IsAny<Order>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(CreateOrderData))]
        public async void CreateOneAsync_ShouldReturnValidResponse(Order repoResponse, UserReadDTO userReadDTO, OrderReadDTO expected, Type? exceptionType)
        {
            var orderRepo = new Mock<IOrderRepo>();
            var userService = new Mock<IUserService>();
            orderRepo.Setup(repo => repo.CreateOneAsync(It.IsAny<Order>())).ReturnsAsync(repoResponse);
            userService.Setup(service => service.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(userReadDTO);
            var orderService = new OrderService(orderRepo.Object, _mapper, userService.Object);
            OrderCreateDTO orderCreateDTO = new OrderCreateDTO
            {
                OrderStatus = OrderStatus.Registered,
                OrderItems = new List<OrderItemCreateDTO>
                {
                    new OrderItemCreateDTO {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItemCreateDTO {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
            };

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => orderService.CreateOrderAsync(Guid.NewGuid(), orderCreateDTO));
            }
            else
            {
                var response = await orderService.CreateOrderAsync(Guid.NewGuid(), orderCreateDTO);
                Assert.Equivalent(expected.OrderStatus, response.OrderStatus);
                Assert.Equivalent(expected.User.FirstName, response.User.FirstName);
            }
        }

        public class CreateOrderData : TheoryData<Order?, UserReadDTO, OrderReadDTO?, Type?>
        {
            public CreateOrderData()
            {
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Registered,
                    OrderNumber = "123456",
                    orderItems = new List<OrderItem>
                {
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItem {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
                };
                User user = new User()
                {
                    FirstName = "Sunil",
                    LastName = "Karki"
                };
                order.User = user;
                UserReadDTO userReadDTO = new UserReadDTO()
                {
                    FirstName = "Sunil",
                    LastName = "Karki"
                };
                OrderReadDTO orderReadDTO = new OrderReadDTO
                {
                    Id = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Registered,
                    OrderNumber = "123456",
                    orderItems = new List<OrderItemReadDTO>
                {
                    new OrderItemReadDTO {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    },
                    new OrderItemReadDTO {
                        Price=10,
                        Quantity=50,
                        ProductId=Guid.NewGuid()
                    }
                }
                };
                orderReadDTO.User = userReadDTO;
                Add(order, userReadDTO, orderReadDTO, null);
            }
        }

    }
}