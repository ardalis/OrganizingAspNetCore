using Microsoft.AspNetCore.Mvc;

namespace WithFeatureFolders.Features.Samurai
{
    [Route("/Samurai/Swords/", Name="SamuraiSwords")]
    public class SwordsController : Controller
    {
        public IActionResult Index()
        {
            var swords = new string[] {"Wakizashi", "Tanto"};

            // this is loading the view from /Features/Ninjas/Swords/Index.cshtml
            return View(swords);
        }
    }
}