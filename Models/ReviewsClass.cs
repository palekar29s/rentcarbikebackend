namespace rentcarbike.Models
{
    public class ReviewsClass
    {

        public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int VehicleId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    }
}
