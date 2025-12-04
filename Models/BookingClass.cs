namespace rentcarbike.Models
{
    public class BookingClass
    {


        public int BookingId { get; set; }

        public string userid { get; set; }

        public string vehicleid { get; set; }

        public string startdate { get; set; }
        public string enddate { get; set; }

        public string totolprice { get; set; }

        public string bookingstatus { get; set; }
    }
}
