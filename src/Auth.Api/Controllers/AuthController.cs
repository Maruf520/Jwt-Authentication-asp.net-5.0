using Auth.Dtos.Users;
using Auth.Models;
using Auth.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            ServiceResponse<int> response = await authService.Register(
                new UserDto
                {
                    Username = userRegisterDto.Username
                },
                userRegisterDto.Password
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }

            response.Success = true;
            response.Message = "Registered Successfully";

            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            ServiceResponse<string> response = await authService.Login(userLoginDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
