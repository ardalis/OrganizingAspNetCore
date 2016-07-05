using DefaultOrganization.Core.Interfaces;
using DefaultOrganization.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace DefaultOrganization.Controllers
{
    public class PlantsController : Controller
    {
        private readonly IRepository<Plant> _plantRepository;

        public PlantsController(IRepository<Plant> plantRepository)
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