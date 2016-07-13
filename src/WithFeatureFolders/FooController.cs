using Microsoft.AspNetCore.Mvc;

namespace WithFeatureFolders
{
    public class FooController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}