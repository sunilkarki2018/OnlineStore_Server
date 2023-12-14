using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Business.src.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepo _repo;
        private ITokenService _tokenService;
        public AuthService(IUserRepo repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService=tokenService;
        }
        public async Task<string> Login(Credentials credentials)
        {
            var foundByEmail = await _repo.FindByEmailAsync(credentials.Email);
            if (foundByEmail is null)
            {
                throw new Exception("not found");
            }
            var isPasswordMatch = PasswordService.VerifyPassword(credentials.Password, foundByEmail.Password, foundByEmail.Salt);
            if (isPasswordMatch)
            {
                return _tokenService.GenerateToken(foundByEmail);
            }
            throw new Exception("not found");
        }
    }
}