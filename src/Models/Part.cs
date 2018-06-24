using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{

    public class Part
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public PartType PartType { get; set; }

        public int PartTypeId { get; set; }

        public Depot Depot { get; set; }
    }

}