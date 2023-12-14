using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}