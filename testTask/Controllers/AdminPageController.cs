using Microsoft.AspNetCore.Mvc;
using testTask.Data.Encoding;
using testTask.Data.Interfaces;
using testTask.Data.Models;
using testTask.Views.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace testTask.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPageController : Controller
    {
        private IAllSights _sights;
        private IAllUsers _users;
        private static User _currentUser;
        private static Sight _currentSight;

        public AdminPageController(IAllSights sights, IAllUsers users)
        {
            _sights = sights;
            _users = users;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users() 
            => View(new AllUsersModel { Users = _users.Users.ToList() });

        public IActionResult EditUser(int itemId)
        {
            _currentUser = _users.Users.FirstOrDefault(i => i.Id == itemId);
            ICollection<SelectListItem> items = _users.GetRoles()
                .OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Value = n.Name,
                    Text = n.Name
                }
                ).ToList();
            UserWithRoles userWithRoles = new UserWithRoles { User = _currentUser, Roles = items };
            return View(userWithRoles);
        }

        [HttpPost]
        public IActionResult EditUser(UserWithRoles userWithRoles)
        {
            _currentUser.FirstName = userWithRoles.User.FirstName;
            _currentUser.LastName = userWithRoles.User.LastName;
            _currentUser.Email = userWithRoles.User.Email;
            _currentUser.Login = userWithRoles.User.Login;

            _users.UpdateUser(_currentUser);

            return RedirectToAction("Users");
        }

        public IActionResult MakeUser(int itemId)
        {
            var user = _users.Users.FirstOrDefault(w => w.Id == itemId);
            if(user != null)
            {
                _users.SetRoleUser(ref user);
            }
            _users.UpdateUser(user);

            return RedirectToAction("Users");
        }

        public IActionResult MakeAdmin(int itemId)
        {
            var user = _users.Users.FirstOrDefault(w => w.Id == itemId);
            if (user != null)
            {
                _users.SetRoleAdmin(ref user);
            }
            _users.UpdateUser(user);

            return RedirectToAction("Users");
        }

        public IActionResult DeleteUser(int itemId)
        {
            _users.DeleteUser(_users.Users.FirstOrDefault(i => i.Id == itemId));
            return RedirectToAction("Users");
        }

        public IActionResult Articles()
            => View(new AllSightsModel { Sights = _sights.Sights.ToList() });

        public IActionResult DeleteArticle(int itemId)
        {
            _sights.DeleteSight(_sights.Sights.FirstOrDefault(i => i.Id == itemId));
            return RedirectToAction("Articles");
        }

        public IActionResult EditArticle(int itemId)
        {
            var sight = _sights.Sights.FirstOrDefault(i => i.Id == itemId);
            _currentSight = sight;
            return View(sight);
        }

        [HttpPost]
        public IActionResult EditArticle(Sight sight)
        {
            _currentSight.Name = sight.Name;
            _currentSight.Description = sight.Description;
            _currentSight.Image = sight.Image;
            _currentSight.Location = sight.Location;

            _sights.UpdateSight(_currentSight);

            return RedirectToAction("Articles");
        }

        public IActionResult AddArticle() => View();


        [HttpPost]
        public IActionResult AddArticle(Sight sight)
        {
            _sights.AddSight(sight);
            return RedirectToAction("Articles");
        }
    }
}
