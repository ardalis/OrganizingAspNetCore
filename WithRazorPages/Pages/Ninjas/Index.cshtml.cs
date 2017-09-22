using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithRazorPages.Core.Interfaces;
using WithRazorPages.Core.Model;

namespace WithRazorPages.Pages.Ninjas
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Ninja> _ninjaRepository;

        public List<NinjaViewModel> Ninjas { get; set; } = new List<NinjaViewModel>();

        public class NinjaViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name} ({Id})";
            }
        }

        public IndexModel(IRepository<Ninja> ninjaRepository)
        {
            _ninjaRepository = ninjaRepository;
        }

        [ResponseCache(Duration = 10)]
        public async Task OnGetAsync()
        {
            Ninjas = _ninjaRepository.List()
                .Select(n => new NinjaViewModel { Id = n.Id, Name = n.Name }).ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var entity = new Ninja()
            {
                Name = "Random Ninja"
            };
            _ninjaRepository.Add(entity);

            return RedirectToPage();
        }

        [PositiveParameter("id")]
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var entityToDelete = _ninjaRepository.GetById(id);
            _ninjaRepository.Delete(entityToDelete);

            return RedirectToPage();
        }
    }
}
