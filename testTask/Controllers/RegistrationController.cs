using Microsoft.AspNetCore.Mvc;
using testTask.Data.Encoding;
using testTask.Data.Interfaces;
using testTask.Data.Models;
using testTask.Models;

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
        public IActionResult Index(RegistrationUserModel user)
        {
            if(ModelState.IsValid)
            {
                var dbUser = new User();

                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.Email = user.Email;
                dbUser.Login = user.Login;
                dbUser.Password = HashPasswordHelper.GetHashPassword(user.Password); ;

                _users.SetRoleUnregistred(ref dbUser);
                _users.AddUser(dbUser);
                return RedirectToAction("Complete");
            }
            return View();
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Регистрация успешно завершена";
            return View();
        }
    }
}
