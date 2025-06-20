using Infobip_sample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication.SignIn.Models;

namespace Infobip_sample.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index",model);

            var user = StaticUsersStore.Users
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                return Redirect("/Home/Index");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View("Index",model);
        }
    }
}
