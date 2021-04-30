using Auth.Dtos.Users;
using Auth.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> CheckUserExistsByName(string username);
        void CreateUserAsync(UserDto user);
        User GetUserByName( string username );
    }
}
