namespace rentcarbike.Models
{
    public class BookingHistoryDto
    {

        public int BookingId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string BookingStatus { get; set; }
    }
}
