using Microsoft.AspNetCore.Mvc;
using WithAreas.Core.Interfaces;
using WithAreas.Core.Model;

namespace DefaultOrganization.Controllers
{
    public class PiratesController : Controller
    {
        private readonly IRepository<Pirate> _pirateRepository;

        public PiratesController(IRepository<Pirate> pirateRepository)
        {
            _pirateRepository = pirateRepository;
        }

        public IActionResult Index()
        {
            var entities = _pirateRepository.List();
            return View(entities);
        }

        public IActionResult Details(int id)
        {
            var entity = _pirateRepository.GetById(id);
            return View(entity);
        }

        public IActionResult Add()
        {
            var entity = new Pirate()
            {
                Name = "Random Pirate"
            };
            _pirateRepository.Add(entity);

            return RedirectToAction("Index");
        }
    }
}