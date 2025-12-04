
namespace rentcarbike.Models
{
    public class VehicleImagesClass
    {

         public int ImageId { get; set; }
    public int VehicleId { get; set; }

    // VARBINARY → byte[]
    public byte[] ImageUrl { get; set; }

    public string VehicleName { get; set; }
    }
}
