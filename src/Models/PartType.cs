using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class PartType
    {
        public int Id { get; set; }

        [Required]
        public string Article { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}