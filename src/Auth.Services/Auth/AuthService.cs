﻿using Auth.Dtos.Users;
using Auth.Models;
using Auth.Models.Users;
using Auth.Repositories.Users;
using Auth.Services.UserExtenstionService;
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
        private readonly IUserExtentionService userExtentionService;
        public AuthService(IUserRepository userRepository, IUserExtentionService userExtentionService)
        {
            this.userRepository = userRepository;
            this.userExtentionService = userExtentionService;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto user)
        {
            ServiceResponse<string> response = new();
            var userToGet = userRepository.GetUserByName(user.UserName);
            if (userToGet == null)
            {
                response.Success = false;
                response.Message = "User Not Found";
                return response;
            }
            else if (!VerifyPasswordHash(user.Password, userToGet.PasswordHash, userToGet.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wromg Password";
            }
            else
            {
                response.Data = await userExtentionService.GenerateAccessToken(userToGet);
            }
            return response;
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


        private bool VerifyPasswordHash(string password, byte[] passswordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }



    }
}
