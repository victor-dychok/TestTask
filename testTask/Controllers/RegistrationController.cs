using Microsoft.AspNetCore.Mvc;
using testTask.Data.Encoding;
using testTask.Data.Interfaces;
using testTask.Data.Models;

namespace testTask.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IAllUsers _users;

        public RegistrationController(IAllUsers users)
        {
            _users = users;
        }

        public IActionResult Index() => View();
        
        [HttpPost]
        public IActionResult Index(User user)
        {
            user.Password = HashPasswordHelper.GetHashPassword(user.Password);
            _users.SetRoleUnregistred(ref user);
            _users.AddUser(user);
            return RedirectToAction("Complete");
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Регистрация успешно завершена";
            return View();
        }
    }
}
