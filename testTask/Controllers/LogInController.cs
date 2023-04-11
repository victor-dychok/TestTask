using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using testTask.Data.Encoding;
using testTask.Data.Interfaces;
using testTask.Data.Models;

namespace testTask.Controllers
{
    public class LogInController : Controller
    {
        private readonly IAllUsers _users;
        private static User _currentUser;

        public LogInController(IAllUsers users)
        {
            _users = users;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(User user)
        {

            var person = _users.Authenticate(user.Login, HashPasswordHelper.GetHashPassword(user.Password));

            if (person != null)
            {
                _currentUser = person;

                await Authorize(_currentUser);

                if (_currentUser.Role.Name == "Admin")
                {
                    return RedirectToAction("Index", "AdminPage");
                }
                else if (_currentUser.Role.Name == "User")
                {
                    return RedirectToAction("Index", "Articles");
                }
                else
                {
                    return RedirectToAction("Complete");
                }
            }

            return View(user);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = $"Вход выполнен с ролью {_currentUser.Role.Name}";
            return View();
        }

        private async Task Authorize(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _users.GetRole(user.Role.Id).Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
