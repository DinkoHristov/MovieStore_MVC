using Microsoft.AspNetCore.Mvc;
using MovieStore.Constants;
using MovieStore.Data.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService authService;

        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await authService.LoginAsync(model);

            if (result.StatusCode == 0)
            {
                TempData["Message"] = MovieStoreConstants.UnsuccessfullyLogin;

                return RedirectToAction(nameof(Login));
            }

            return RedirectToAction(nameof(HomeController.Index), MovieStoreConstants.HomeControllerName);
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await authService.RegisterAsync(model);

            if (result.StatusCode == 0)
            {
                TempData["Message"] = MovieStoreConstants.UnsuccessfullyRegistration;

                return RedirectToAction(nameof(Register));
            }

            return RedirectToAction(nameof(Login));
        }
    }
}