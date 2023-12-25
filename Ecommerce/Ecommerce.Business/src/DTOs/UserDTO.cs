using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class UserReadDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Avatar Avatar { get; set; }
        public Role Role { get; set; }
        public AddressReadDTO AddressReadDTO { get; set; }
    }

    public class UserUpdateDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AvatarUpdateDTO AvatarUpdateDTO { get; set; }
        public Role Role { get; set; }
        public AddressUpdateDTO AddressUpdateDTO { get; set; }
    }

    public class UserCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public AvatarCreateDTO AvatarCreateDTO { get; set; }
        public AddressCreateDTO AddressCreateDTO { get; set; }
    }
    public class PaginatedUserReadDTO
    {
        public IEnumerable<UserReadDTO> Users { get; set; }
        public decimal PageCount { get; set; }
    }
}