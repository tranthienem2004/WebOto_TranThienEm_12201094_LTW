namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string ContactPhone { get; set; }
        public string FormattedPrice => $"{Price:N0} triệu";
        public string FormattedMileage => $"{Mileage:N0} Km";
    }
}