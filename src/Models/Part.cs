namespace CarShowRoom.Models
{

    public class Part
    {
        public int Id { get; set; }        
        
        public string Code { get; set; }

        public PartType PartType { get; set; }

        public Depot Depot { get; set; }
    }

}