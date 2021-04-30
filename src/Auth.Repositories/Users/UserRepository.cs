using Auth.Dtos.Users;
using Auth.Models;
using Auth.Models.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext context;
        private readonly IMapper mapper;
        public UserRepository(AuthDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<bool> CheckUserExistsByName(string username)
        {
            if ( context.Users.Any(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public void CreateUserAsync(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void CreateUserAsync(UserDto user)
        {
            var usertomap = mapper.Map<User>(user);
            context.Users.Add(usertomap);
            context.SaveChanges();
        }
    }
}
