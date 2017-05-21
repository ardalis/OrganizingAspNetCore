using Microsoft.AspNetCore.Mvc;

namespace WithFeatureFolders.Features.Ninjas
{
    [Route("/Ninjas/Swords/", Name ="NinjaSwords")]
    public class SwordsController : Controller
    {
        public IActionResult Index()
        {
            var swords = new string[] {"Katana", "Ninjago"};
            return View(swords);
        }
    }
}