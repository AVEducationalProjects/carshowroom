using CarShowRoom.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EngineCapacity { get; set; }

        public EngineType EngineType { get; set; }

        public DriveUnitType DriveUnitType { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public Vendor Vendor { get; set; }

        public int VendorId { get; set; }

        public override string ToString()
        {
            return $"{Vendor.Name} {Name} ({EngineCapacity}, {EngineType.GetAttribute<DisplayAttribute>().Name.ToLower()}, кпп {TransmissionType.GetAttribute<DisplayAttribute>().Name.ToLower()}, привод {DriveUnitType.GetAttribute<DisplayAttribute>().Name.ToLower()})";
        }
    }
}