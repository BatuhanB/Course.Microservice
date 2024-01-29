using System.Linq;
using System.Threading.Tasks;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace Course.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignupDto model)
        {
            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                City = model.City
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) return NoContent();
            
            var errors = result.Errors.Select(x => x.Description).ToList();
            
            return BadRequest(Response<NoContent>.Fail(errors,404));
        }
    }
}