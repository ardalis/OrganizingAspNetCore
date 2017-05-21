using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WithAreas.Areas.Ninjas.ViewModels;
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

            var viewModel = new NinjaListViewModel();
            viewModel.Ninjas.AddRange(entities
                .Select(e => new NinjaViewModel()
                { Id = e.Id, Name = e.Name })
                .ToList());
            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var entity = _ninjaRepository.GetById(id);

            var viewmodel = new NinjaViewModel() { Id = entity.Id, Name = entity.Name };
            return View(viewmodel);
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