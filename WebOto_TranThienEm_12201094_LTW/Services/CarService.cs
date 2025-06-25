    using Microsoft.EntityFrameworkCore;
    using WebOto_TranThienEm_12201094_LTW.Models;
    using WebOto_TranThienEm_12201094_LTW.Models.Data;
    using WebOto_TranThienEm_12201094_LTW.Models.ViewModels;

    namespace WebOto_TranThienEm_12201094_LTW.Services
    {
        public class CarService : ICarService
        {
            private readonly CarDbContext _context;

            public CarService(CarDbContext context)
            {
                _context = context;
            }
            public async Task<List<CarViewModel>> GetAllCarsAsync()
            {
                return await _context.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Category)
                    .Where(c => c.IsAvailable)
                    .Select(c => new CarViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BrandName = c.Brand.Name,
                        CategoryName = c.Category.Name,
                        Year = c.Year,
                        Price = c.Price,
                        Mileage = c.Mileage,
                        FuelType = c.FuelType,
                        Transmission = c.Transmission,
                        Color = c.Color,
                        ImageUrl = c.ImageUrl,
                        Location = c.Location,
                        ContactPhone = c.ContactPhone
                    })
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }

            // CẬP NHẬT PHƯƠNG THỨC NÀY
            public async Task<List<CarViewModel>> GetFeaturedCarsAsync(int count = 6)
            {
                return await _context.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Category)
                    .Where(c => c.IsAvailable && c.IsFeatured)
                    .OrderByDescending(c => c.CreatedDate)
                    .Take(count)
                    .Select(c => new CarViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BrandName = c.Brand.Name,
                        CategoryName = c.Category.Name,
                        Year = c.Year,
                        Price = c.Price,
                        Mileage = c.Mileage,
                        FuelType = c.FuelType,
                        Transmission = c.Transmission,
                        Color = c.Color,
                        ImageUrl = c.ImageUrl,
                        Location = c.Location,
                        ContactPhone = c.ContactPhone
                    })
                    .ToListAsync();
            }

            public async Task<List<CarViewModel>> GetLatestCarsAsync(int count = 6)
            {
                return await _context.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Category)
                    .Where(c => c.IsAvailable)
                    .OrderByDescending(c => c.CreatedDate)
                    .Take(count)
                    .Select(c => new CarViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BrandName = c.Brand.Name,
                        CategoryName = c.Category.Name,
                        Year = c.Year,
                        Price = c.Price,
                        Mileage = c.Mileage,
                        FuelType = c.FuelType,
                        Transmission = c.Transmission,
                        Color = c.Color,
                        ImageUrl = c.ImageUrl,
                        Location = c.Location,
                        ContactPhone = c.ContactPhone
                    })
                    .ToListAsync();
            }

            public async Task<CarViewModel> GetCarByIdAsync(int id)
            {
                return await _context.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Category)
                    .Where(c => c.Id == id && c.IsAvailable)
                    .Select(c => new CarViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BrandName = c.Brand.Name,
                        CategoryName = c.Category.Name,
                        Year = c.Year,
                        Price = c.Price,
                        Mileage = c.Mileage,
                        FuelType = c.FuelType,
                        Transmission = c.Transmission,
                        Color = c.Color,
                        ImageUrl = c.ImageUrl,
                        Location = c.Location,
                        ContactPhone = c.ContactPhone
                    })
                    .FirstOrDefaultAsync();
            }

            public async Task<SearchViewModel> SearchCarsAsync(SearchViewModel searchModel)
            {
                var query = _context.Cars
                    .Include(c => c.Brand)
                    .Include(c => c.Category)
                    .Where(c => c.IsAvailable);
                if (!string.IsNullOrEmpty(searchModel.Keyword))
                {
                    query = query.Where(c => c.Name.Contains(searchModel.Keyword) ||
                                             c.Brand.Name.Contains(searchModel.Keyword));
                }
                if (searchModel.BrandId.HasValue)
                {
                    query = query.Where(c => c.BrandId == searchModel.BrandId.Value);
                }
                if (searchModel.CategoryId.HasValue)
                {
                    query = query.Where(c => c.CategoryId == searchModel.CategoryId.Value);
                }
                if (searchModel.YearFrom.HasValue)
                {
                    query = query.Where(c => c.Year >= searchModel.YearFrom.Value);
                }

                if (searchModel.YearTo.HasValue)
                {
                    query = query.Where(c => c.Year <= searchModel.YearTo.Value);
                }

                if (searchModel.PriceFrom.HasValue)
                {
                    query = query.Where(c => c.Price >= searchModel.PriceFrom.Value);
                }

                if (searchModel.PriceTo.HasValue)
                {
                    query = query.Where(c => c.Price <= searchModel.PriceTo.Value);
                }
                if (!string.IsNullOrEmpty(searchModel.FuelType))
                {
                    query = query.Where(c => c.FuelType == searchModel.FuelType);
                }

                if (!string.IsNullOrEmpty(searchModel.Transmission))
                {
                    query = query.Where(c => c.Transmission == searchModel.Transmission);
                }

                if (!string.IsNullOrEmpty(searchModel.Location))
                {
                    query = query.Where(c => c.Location.Contains(searchModel.Location));
                }

                searchModel.TotalCars = await query.CountAsync();

                var cars = await query
                    .OrderByDescending(c => c.CreatedDate)
                    .Select(c => new CarViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BrandName = c.Brand.Name,
                        CategoryName = c.Category.Name,
                        Year = c.Year,
                        Price = c.Price,
                        Mileage = c.Mileage,
                        FuelType = c.FuelType,
                        Transmission = c.Transmission,
                        Color = c.Color,
                        ImageUrl = c.ImageUrl,
                        Location = c.Location,
                        ContactPhone = c.ContactPhone
                    })
                    .ToListAsync();

                searchModel.Cars = cars;
                searchModel.Brands = await GetAllBrandsAsync();
                searchModel.Categories = await GetAllCategoriesAsync();

                return searchModel;
            }

            public async Task<List<Brand>> GetAllBrandsAsync()
            {
                return await _context.Brands.OrderBy(b => b.Name).ToListAsync();
            }

            public async Task<List<Category>> GetAllCategoriesAsync()
            {
                return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            }

            public async Task<bool> AddCarAsync(Car car)
            {
                try
                {
                    _context.Cars.Add(car);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<bool> UpdateCarAsync(Car car)
            {
                try
                {
                    _context.Cars.Update(car);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<bool> DeleteCarAsync(int id)
            {
                try
                {
                    var car = await _context.Cars.FindAsync(id);
                    if (car != null)
                    {
                        car.IsAvailable = false; // Soft delete
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<int> GetTotalCarsCountAsync()
            {
                return await _context.Cars.Where(c => c.IsAvailable).CountAsync();
            }
        }
    }