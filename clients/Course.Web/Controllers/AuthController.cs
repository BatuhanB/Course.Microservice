using Course.Web.Models;
using Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Course.Web.Controllers
{
    public class AuthController(IIdentityService identityService) : Controller
    {
        private readonly IIdentityService _identityService = identityService;

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SigIn(model);

            if(!response.IsSuccessful)
            {
                response.Errors.ForEach(e =>
                {
                    ModelState.AddModelError(String.Empty,e);
                });
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
