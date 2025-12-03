namespace rentcarbike.Models
{
    public class VehicleClass
    {
         public int Id { get; set; } 
        public string Name { get; set; }
        public string type { get; set; }

        public string brand { get; set; }

        public string model { get; set;}

        public string priceperhour { get; set; }

        public string priceperday { get; set;}

        public string fueltype { get; set;}

        public string transmission { get; set;}

        public string status { get; set;}
    }
}
