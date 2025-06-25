namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarViewModel Car { get; set; }
        public List<CarViewModel> RelatedCars { get; set; } = new List<CarViewModel>();
        public List<string> ImageGallery { get; set; } = new List<string>();
        public ContactViewModel ContactInfo { get; set; } = new ContactViewModel();
    }
}