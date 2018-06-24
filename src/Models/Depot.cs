using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class Depot
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}