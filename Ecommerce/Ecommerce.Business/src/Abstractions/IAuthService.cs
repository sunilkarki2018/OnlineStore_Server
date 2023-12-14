using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Shared;

namespace Ecommerce.Business.src.Abstractions
{
    public interface IAuthService
    {
        Task<string> Login(Credentials credentials);
    }
}