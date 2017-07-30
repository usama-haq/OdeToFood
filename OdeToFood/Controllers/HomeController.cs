using Microsoft.AspNetCore.Mvc;
using OdeToFood.Contracts;
using OdeToFood.Entities;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _greeter = greeter;
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Respond only to HTTP Get request
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Respond only to HTTP Post request
        [HttpPost]
        // To prevent Cross-Site-Scripting
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Cuisine = model.Cuisine;
                newRestaurant.Name = model.Name;

                _restaurantData.Add(newRestaurant);

                return RedirectToAction(nameof(Details), new { Id = newRestaurant.Id });
            }
            else
            {
                return View();
            }
        }
    }
}