using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Shared
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }
        public static CustomException NotFoundException(string msg = "Not found")
        {
            return new CustomException(404, msg);
        }
        public static CustomException DuplicateEmailException(string msg = "Email already exist")
        {
            return new CustomException(409, msg);
        }
        public static CustomException ProductNotAvailableException(string msg = "Product is not available")
        {
            return new CustomException(400, msg);
        }
        public static CustomException InvalidLoginCredentialsException(string msg = "Invalid Login Credentials")
        {
            return new CustomException(401, msg);
        }
    }
}