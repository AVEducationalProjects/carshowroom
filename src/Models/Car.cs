namespace CarShowRoom.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string VIN { get; set; }

        public int Year { get; set; }
        
        public decimal Price { get; set; }
        
        public CarColor Color { get; set; }
        
        public bool TestDrive { get; set; }

        public CarModel CarModel { get; set; }
        
    }
    
}