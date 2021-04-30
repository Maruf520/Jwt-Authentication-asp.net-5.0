using Auth.Dtos.Token;
using Auth.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.UserExtenstionService
{
    public interface IUserExtentionService
    {
        Task<string> GenerateAccessToken(User user);
    }
}
