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
    }
}