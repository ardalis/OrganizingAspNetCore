using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithRazorPages.Core.Interfaces;
using WithRazorPages.Core.Model;

namespace WithRazorPages.Pages.Pirates
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Pirate> _pirateRepository;

        public List<PirateViewModel> Pirates { get; set; } = new List<PirateViewModel>();

        public class PirateViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name} ({Id})";
            }
        }

        public IndexModel(IRepository<Pirate> pirateRepository)
        {
            _pirateRepository = pirateRepository;
        }

        public async Task OnGetAsync()
        {
            Pirates = _pirateRepository.List()
                .Select(n => new PirateViewModel { Id = n.Id, Name = n.Name }).ToList();
        }
        public async Task OnGetAddAsync()
        {
            var entity = new Pirate()
            {
                Name = "Random Pirate"
            };
            _pirateRepository.Add(entity);

            await OnGetAsync();
        }

    }
}
