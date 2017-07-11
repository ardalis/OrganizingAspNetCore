using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithRazorPages.Core.Interfaces;
using WithRazorPages.Core.Model;

namespace WithRazorPages.Pages.Zombies
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Zombie> _zombieRepository;

        public List<ZombieViewModel> Zombies { get; set; } = new List<ZombieViewModel>();

        public class ZombieViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name} ({Id})";
            }
        }

        public IndexModel(IRepository<Zombie> zombieRepository)
        {
            _zombieRepository = zombieRepository;
        }

        public async Task OnGetAsync()
        {
            Zombies = _zombieRepository.List()
                .Select(n => new ZombieViewModel { Id = n.Id, Name = n.Name }).ToList();
        }

    }
}
