using Microsoft.AspNetCore.Mvc;
using WebOto_TranThienEm_12201094_LTW.Models.ViewModels;
using WebOto_TranThienEm_12201094_LTW.Services;
using WebOto_TranThienEm_12201094_LTW.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebOto_TranThienEm_12201094_LTW.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        // GET: Cars
        public async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            try
            {
                if (searchModel == null)
                    searchModel = new SearchViewModel();

                var result = await _carService.SearchCarsAsync(searchModel);
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cars index");
                // Trả về một SearchViewModel rỗng hoặc thông báo lỗi thân thiện
                return View(new SearchViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                if (car == null)
                {
                    TempData["Error"] = "Không tìm thấy xe.";
                    return RedirectToAction("Index");
                }

                var relatedCars = await _carService.GetFeaturedCarsAsync(4);

                var model = new CarDetailsViewModel
                {
                    Car = car,
                    RelatedCars = relatedCars
                        .Where(c => c.Id != car.Id)

                        .Take(3)
                        .ToList()
                };

                return View("Details", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tải chi tiết xe có ID: {CarId}", id);
                TempData["Error"] = "Đã xảy ra lỗi khi tải chi tiết xe.";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Search(SearchViewModel searchModel)
        {
            try
            {
                if (searchModel == null)
                    searchModel = new SearchViewModel();

                var result = await _carService.SearchCarsAsync(searchModel);

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_CarSearchResults", result);
                }

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching cars");
                return View(new SearchViewModel());
            }
        }
        public async Task<IActionResult> ByBrand(int brandId, int page = 1)
        {
            try
            {
                var searchModel = new SearchViewModel
                {
                    BrandId = brandId,
                    PageNumber = page
                };

                var result = await _carService.SearchCarsAsync(searchModel);
                return View("Index", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cars by brand: {BrandId}", brandId);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ByCategory(int categoryId, int page = 1)
        {
            try
            {
                var searchModel = new SearchViewModel
                {
                    CategoryId = categoryId,
                    PageNumber = page
                };

                var result = await _carService.SearchCarsAsync(searchModel);
                return View("Index", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cars by category: {CategoryId}", categoryId);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> QuickSearch(string keyword)
        {
            try
            {
                var searchModel = new SearchViewModel
                {
                    Keyword = keyword
                };

                var result = await _carService.SearchCarsAsync(searchModel);

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = true,
                        cars = result.Cars.Take(5).Select(c => new
                        {
                            id = c.Id,
                            name = c.Name,
                            brandName = c.BrandName,
                            price = c.FormattedPrice,
                            imageUrl = c.ImageUrl
                        })
                    });
                }

                return RedirectToAction("Search", searchModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in quick search");
                return Json(new { success = false });
            }
        }
        public async Task<IActionResult> Compare(string carIds)
        {
            try
            {
                if (string.IsNullOrEmpty(carIds))
                {
                    return RedirectToAction("Index");
                }

                var ids = carIds.Split(',').Select(int.Parse).ToList();
                if (ids.Count > 3)
                {
                    TempData["Error"] = "Chỉ có thể so sánh tối đa 3 xe cùng lúc.";
                    return RedirectToAction("Index");
                }

                var cars = new List<CarViewModel>();
                foreach (var id in ids)
                {
                    var car = await _carService.GetCarByIdAsync(id);
                    if (car != null)
                        cars.Add(car);
                }

                return View(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comparing cars");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFilterOptions()
        {
            try
            {
                var brands = await _carService.GetAllBrandsAsync();
                var categories = await _carService.GetAllCategoriesAsync();

                return Json(new
                {
                    brands = brands.Select(b => new { id = b.Id, name = b.Name }),
                    categories = categories.Select(c => new { id = c.Id, name = c.Name })
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting filter options");
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var categories = await _carService.GetAllCategoriesAsync();
                var brands = await _carService.GetAllBrandsAsync();

                var viewModel = new CarCreateEditViewModel
                {
                    Categories = categories.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }),
                    Brands = brands.Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Name
                    })
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading car creation form.");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarCreateEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var car = new Car
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        ImageUrl = model.ImageUrl,
                        Year = model.Year,
                        Mileage = model.Mileage,
                        FuelType = model.FuelType,
                        Transmission = model.Transmission,
                        Color = model.Color,
                        Location = model.Location,
                        ContactPhone = model.ContactPhone,
                        IsAvailable = model.IsAvailable,
                        CategoryId = model.CategoryId,
                        BrandId = model.BrandId,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _carService.AddCarAsync(car);
                    TempData["SuccessMessage"] = "Xe mới đã được thêm thành công!";
                    return RedirectToAction(nameof(Index));
                }
                var categories = await _carService.GetAllCategoriesAsync();
                var brands = await _carService.GetAllBrandsAsync();

                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
                model.Brands = brands.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                });

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new car.");
                ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm xe. Vui lòng thử lại.");
                var categories = await _carService.GetAllCategoriesAsync();
                var brands = await _carService.GetAllBrandsAsync();
                model.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                model.Brands = brands.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name });
                return View(model);
            }
        }
    }
}