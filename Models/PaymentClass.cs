namespace rentcarbike.Models
{
    public class PaymentClass
    {
        public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
    public DateTime PaymentDate { get; set; }
    }
}
