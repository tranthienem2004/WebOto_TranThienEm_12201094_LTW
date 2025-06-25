using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebOto_TranThienEm_12201094_LTW.Models.ViewModels;
using WebOto_TranThienEm_12201094_LTW.Services;

namespace WebOto_TranThienEm_12201094_LTW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carService;

        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new HomeViewModel();

                model.FeaturedCars = await _carService.GetFeaturedCarsAsync(6);

                model.LatestCars = await _carService.GetLatestCarsAsync(8);

                model.PopularBrands = await _carService.GetAllBrandsAsync();

                model.Categories = await _carService.GetAllCategoriesAsync();

                model.TotalCars = await _carService.GetTotalCarsCountAsync();

                model.SearchModel = new SearchViewModel
                {
                    Brands = model.PopularBrands,
                    Categories = model.Categories
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");
                return View(new HomeViewModel());
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Success"] = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hổi sớm nhất có thể.";
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing contact form");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi gửi thông tin. Vui lòng thử lại.");
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> QuickSearch(string term)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    return Json(new { suggestions = new List<object>() });
                }

                var searchModel = new SearchViewModel
                {
                    Keyword = term,
                    PageSize = 5
                };

                var result = await _carService.SearchCarsAsync(searchModel);

                var suggestions = result.Cars.Select(c => new
                {
                    id = c.Id,
                    name = c.Name,
                    brand = c.BrandName,
                    price = c.FormattedPrice,
                    year = c.Year,
                    image = c.ImageUrl ?? "/images/default-car.jpg"
                }).ToList();

                return Json(new { suggestions });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in quick search");
                return Json(new { suggestions = new List<object>() });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var totalCars = await _carService.GetTotalCarsCountAsync();
                var brands = await _carService.GetAllBrandsAsync();
                var categories = await _carService.GetAllCategoriesAsync();

                return Json(new
                {
                    totalCars,
                    totalBrands = brands.Count,
                    totalCategories = categories.Count,
                    newCarsThisMonth = 0
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting stats");
                return Json(new { error = "Unable to load stats" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}