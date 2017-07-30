using Microsoft.AspNetCore.Mvc;
using OdeToFood.Model;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Resturant model = new Resturant { Id = 1, Name = "The House of Kobe" };
            return new ObjectResult(model); // Return Object Result : as JSON
        }
    }
}