using WebOto_TranThienEm_12201094_LTW.Models;
using WebOto_TranThienEm_12201094_LTW.Models.ViewModels;

namespace WebOto_TranThienEm_12201094_LTW.Services
{
    public interface ICarService
    {
        Task<List<CarViewModel>> GetAllCarsAsync();
        Task<List<CarViewModel>> GetFeaturedCarsAsync(int count = 6);
        Task<List<CarViewModel>> GetLatestCarsAsync(int count = 6);
        Task<CarViewModel> GetCarByIdAsync(int id);
        Task<SearchViewModel> SearchCarsAsync(SearchViewModel searchModel);
        Task<List<Brand>> GetAllBrandsAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool> AddCarAsync(Car car);
        Task<bool> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(int id);
        Task<int> GetTotalCarsCountAsync();

    }
}