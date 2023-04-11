using Microsoft.AspNetCore.Mvc;
using System.Data;
using testTask.Data.Interfaces;
using testTask.Data.Models;
using testTask.Views.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace testTask.Controllers
{
    [Authorize(Roles = "User")]
    public class ArticlesController : Controller
    {
        private readonly IAllSights _sights;
        private static Sight _currentSight;
        private static bool _isSorted = false;
        private static string _filter = string.Empty;

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

        public IActionResult Index()
        {

            var allSightsList = _isSorted ? _sights.Sights.OrderBy(i => i.Name).ToList() : _sights.Sights.ToList();

            if(_filter != string.Empty)
            {
                var temp = new List<Sight>();
                foreach(var item in allSightsList)
                {
                    if(item.Location.Region == _filter)
                    {
                        temp.Add(item);
                    }
                }
                allSightsList = temp;
            }
            var vm = new AllSightsModel
            {
                Sights = allSightsList,
                Regions = _sights.GetOriginalRegions()
            };
            return View(vm);
        }


        public IActionResult SortedByName()
        {
            _isSorted = true;
            return RedirectToAction("Index");
        }

        public IActionResult FilterByRegion(int itemId)
        {
            _filter = _sights.GetOriginalRegions().First(i => i.Id == itemId).Region;
            return RedirectToAction("Index");
        }

        public IActionResult RemoveRegionFilter()
        {
            _filter = string.Empty;
            return RedirectToAction("Index");
        }
    }
}
