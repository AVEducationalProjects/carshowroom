using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class CarColor
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte Red { get; set; }
        [Required]
        public byte Green { get; set; }
        [Required]
        public byte Blue { get; set; }
    }
}