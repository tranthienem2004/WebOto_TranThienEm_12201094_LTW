namespace WebOto_TranThienEm_12201094_LTW.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public string CustomerName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string CancelReason { get; set; }

    }
}