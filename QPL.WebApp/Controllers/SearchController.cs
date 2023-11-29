using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QPL.WebApp.Controllers
{
    //[Authorize]
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExcIndex()
        {
            return View();
        }
    }
}
