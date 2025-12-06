namespace rentcarbike.Models
{
    public class VehicleClass
    {
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public decimal priceperhour { get; set; }
        public decimal priceperday { get; set; }
        public string fueltype { get; set; }
        public string transmission { get; set; }
        public string imageUrl { get; set; }
   
        public string status { get; set; }

        public List<VehicleImagesClass> Images { get; set; }


    }
   
}
