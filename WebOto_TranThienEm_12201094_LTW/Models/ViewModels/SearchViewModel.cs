namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class SearchViewModel
    {
        public string Keyword { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Location { get; set; }

        public List<Brand> Brands { get; set; } = new List<Brand>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<CarViewModel> Cars { get; set; } = new List<CarViewModel>();

        public int TotalCars { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalPages => (int)Math.Ceiling((double)TotalCars / PageSize);
    }
}
