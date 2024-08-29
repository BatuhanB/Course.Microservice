using Course.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Course.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SharedController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return BadRequest();

            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                City = user.City,
                Email = user.Email
            });
        }
    }
}
