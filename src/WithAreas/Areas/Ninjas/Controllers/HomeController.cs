using Microsoft.AspNetCore.Mvc;
using WithAreas.Core.Interfaces;
using WithAreas.Core.Model;

namespace WithAreas.Areas.Ninjas.Controllers
{
    [Area("Ninjas")]
    public class HomeController : Controller
    {
        private readonly IRepository<Ninja> _ninjaRepository;

        public HomeController(IRepository<Ninja> ninjaRepository)
        {
            _ninjaRepository = ninjaRepository;
        }

        public IActionResult Index()
        {
            var entities = _ninjaRepository.List();
            return View(entities);
        }

        public IActionResult Details(int id)
        {
            var entity = _ninjaRepository.GetById(id);
            return View(entity);
        }

        public IActionResult Add()
        {
            var entity = new Ninja()
            {
                Name = "Random Ninja"
            };
            _ninjaRepository.Add(entity);

            return RedirectToAction("Index");
        }
    }
}