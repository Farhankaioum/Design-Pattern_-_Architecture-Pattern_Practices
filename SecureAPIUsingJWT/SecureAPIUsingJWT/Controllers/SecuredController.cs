using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SecureAPIUsingJWT.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SecuredController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok("This secured data is available only for Authenticated Users.");
        }

        [HttpPost]
        [Authorize(Roles ="Moderator")]
        public async Task<IActionResult> PostSecuredData()
        {
            return Ok("This secured Data is available only for Authenticated users.");
        }
    }
}
