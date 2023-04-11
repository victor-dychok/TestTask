using Microsoft.AspNetCore.Mvc;
using testTask.Data.Interfaces;
using testTask.Data.Models;
using testTask.Views.ViewModel;

namespace testTask.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IAllSights _sights;
        private static Sight _currentSight;

        public ArticlesController(IAllSights sights)
        {
            _sights = sights;
        }

        public IActionResult AllSights(int itemId)
        {
            _currentSight = _sights.Sights.FirstOrDefault(i => i.Id == itemId);
            return RedirectToAction("Article");
        }

        public IActionResult Article() => View(_currentSight);

        public ActionResult Index()
        {
            var vm = new AllSightsModel { Sights = _sights.Sights.ToList()};
            return View(vm);
        }
    }
}
