using Microsoft.AspNetCore.Mvc;
using OdeToFood.Contracts;
using OdeToFood.Entities;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                Restaurants = _restaurantData.GetAll(),
            };
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
                var newRestaurant = new Restaurant()
                {
                    Cuisine = model.Cuisine,
                    Name = model.Name
                };

                newRestaurant = _restaurantData.Add(newRestaurant);
                _restaurantData.Commit();

                return RedirectToAction(nameof(Details), new { Id = newRestaurant.Id });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            var restaurant = _restaurantData.Get(id);

            if (ModelState.IsValid)
            {
                restaurant.Cuisine = model.Cuisine;
                restaurant.Name = model.Name;
                _restaurantData.Commit();
                return RedirectToAction(nameof(Details), new { id = restaurant.Id });
            }
            else
            {
                return View(restaurant);
            }
        }
    }
}