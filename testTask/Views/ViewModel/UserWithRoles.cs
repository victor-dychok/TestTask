using Microsoft.AspNetCore.Mvc.Rendering;
using testTask.Data.Models;

namespace testTask.Views.ViewModel
{
    public class UserWithRoles
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set;}
    }
}
