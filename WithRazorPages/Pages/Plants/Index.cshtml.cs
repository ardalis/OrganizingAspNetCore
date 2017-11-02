using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithRazorPages.Core.Interfaces;
using WithRazorPages.Core.Model;

namespace WithRazorPages.Pages.Plants
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Plant> _plantRepository;

        public List<PlantViewModel> Plants { get; set; } = new List<PlantViewModel>();

        public class PlantViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name} ({Id})";
            }
        }

        public IndexModel(IRepository<Plant> plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public void OnGet()
        {
            Plants = _plantRepository.List()
                .Select(n => new PlantViewModel { Id = n.Id, Name = n.Name }).ToList();
        }
        public void OnGetAdd()
        {
            var entity = new Plant()
            {
                Name = "Random Plant"
            };
            _plantRepository.Add(entity);

            OnGet();
        }

    }
}
