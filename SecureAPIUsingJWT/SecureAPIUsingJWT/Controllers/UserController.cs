using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureAPIUsingJWT.Contexts;
using SecureAPIUsingJWT.Entities;
using SecureAPIUsingJWT.Models;
using SecureAPIUsingJWT.Services;

namespace SecureAPIUsingJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager , IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }
       
        // For get token
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }

        // For get refresh token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var response = await _userService.RefreshTokenAsync(refreshToken);

            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        // set refresh token in cookie
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        // Register user
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        // Find user by Id
        [Authorize]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        // For add role
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
    }
}
