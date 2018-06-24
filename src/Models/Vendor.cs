using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}