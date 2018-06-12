namespace CarShowRoom.Models
{
    public class Car
    {
        public Car()
        {
            Sold = false;
        }

        public int Id { get; set; }

        public string VIN { get; set; }

        public int Year { get; set; }
        
        public decimal Price { get; set; }
        
        public CarColor Color { get; set; }

        public int ColorId { get; set; }

        public bool TestDrive { get; set; }

        public bool Sold { get; set; }

        public CarModel CarModel { get; set; }

        public int CarModelId { get; set; }

        public Depot Depot { get; set; }

        public int? DepotId { get; set; }

        public Client Client { get; set; }

        public int? ClientId { get; set; }

        public Partner Partner { get; set; }

        public int PartnerId { get; set; }

        public override string ToString()
        {
            return $"{CarModel?.ToString()}, {Color?.Name}, {Year}, VIN {VIN}";
        }

    }
    
}