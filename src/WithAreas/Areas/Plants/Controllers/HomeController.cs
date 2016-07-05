using Microsoft.AspNetCore.Mvc;
using WithAreas.Core.Interfaces;
using WithAreas.Core.Model;

namespace WithAreas.Areas.Plants.Controllers
{
    [Area("Plants")]
    public class HomeController : Controller
    {
        private readonly IRepository<Plant> _plantRepository;

        public HomeController(IRepository<Plant> plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public IActionResult Index()
        {
            var entities = _plantRepository.List();
            return View(entities);
        }

        public IActionResult Details(int id)
        {
            var entity = _plantRepository.GetById(id);
            return View(entity);
        }

        public IActionResult Add()
        {
            var entity = new Plant()
            {
                Name = "Random Plant"
            };
            _plantRepository.Add(entity);

            return RedirectToAction("Index");
        }
    }
}