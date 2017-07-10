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

        public async Task OnGetAsync()
        {
            Ninjas = _ninjaRepository.List()
                .Select(n => new NinjaViewModel { Id = n.Id, Name = n.Name }).ToList();
        }

    }
}
