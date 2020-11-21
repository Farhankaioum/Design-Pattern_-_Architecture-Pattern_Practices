using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityservice _identityservice;

        public IdentityController(IIdentityservice identityservice)
        {
            _identityservice = identityservice;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authService = await _identityservice.RegisterAsync(request.Email, request.Password);

            if (!authService.Success)
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authService.Errors
                });

            return Ok(new AuthSuccessResponse
            {
                Token = authService.Token
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
          var loginUser = await _identityservice.LoginAsync(request.Email, request.Password);

            if (!loginUser.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = loginUser.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = loginUser.Token
            });
        }
    }
}
