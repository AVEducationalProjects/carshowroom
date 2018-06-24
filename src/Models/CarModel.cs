using CarShowRoom.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EngineCapacity { get; set; }

        [Required]
        public EngineType EngineType { get; set; }

        [Required]
        public DriveUnitType DriveUnitType { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }

        public Vendor Vendor { get; set; }

        [Required]
        public int VendorId { get; set; }

        public override string ToString()
        {
            return $"{Vendor?.Name} {Name} ({EngineCapacity}, {EngineType.GetAttribute<DisplayAttribute>().Name.ToLower()}, кпп {TransmissionType.GetAttribute<DisplayAttribute>().Name.ToLower()}, привод {DriveUnitType.GetAttribute<DisplayAttribute>().Name.ToLower()})";
        }
    }
}