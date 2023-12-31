using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class UserReadDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AvatarReadDTO Avatar { get; set; }
        public Role Role { get; set; }
        public AddressReadDTO Address { get; set; }
    }

    public class UserUpdateDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AvatarUpdateDTO Avatar { get; set; }
        public Role Role { get; set; }
        public AddressUpdateDTO Address { get; set; }
    }

    public class UserCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AvatarCreateDTO Avatar { get; set; }=new AvatarCreateDTO();
        public AddressCreateDTO Address { get; set; }= new AddressCreateDTO();
    }
    public class PaginatedUserReadDTO
    {
        public IEnumerable<UserReadDTO> Users { get; set; }
        public decimal PageCount { get; set; }
    }
}