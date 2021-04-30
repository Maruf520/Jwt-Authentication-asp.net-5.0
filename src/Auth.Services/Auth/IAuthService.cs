using Auth.Dtos.Users;
using Auth.Models;
using Auth.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.Auth
{
    public interface IAuthService
    {

        Task<ServiceResponse<int>> Register (UserDto userDto, string password);
        Task<ServiceResponse<string>> Login (UserLoginDto user);
    }
}
