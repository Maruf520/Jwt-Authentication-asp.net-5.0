using Auth.Dtos.Users;
using Auth.Models;
using Auth.Models.Users;
using Auth.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<ServiceResponse<int>> Register(UserDto user, string password)
        {
            ServiceResponse<int> response = new();
            if (userRepository.CheckUserExistsByName(user.Username).Result == true)
            {
                response.Success = false;
                response.Message = "User Already Exists";
                return response;
            }

            CreateHashPassword(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            userRepository.CreateUserAsync(user);
            return response;
        }


        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
