using System.Linq;
using System.Threading.Tasks;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.IdentityServer
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto model)
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