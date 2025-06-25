namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<CarViewModel> FeaturedCars { get; set; } = new List<CarViewModel>();
        public List<CarViewModel> LatestCars { get; set; } = new List<CarViewModel>();
        public List<Brand> PopularBrands { get; set; } = new List<Brand>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public int TotalCars { get; set; }
        public SearchViewModel SearchModel { get; set; } = new SearchViewModel();
    }
}