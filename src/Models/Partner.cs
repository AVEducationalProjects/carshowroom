using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class Partner
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Requisites { get; set; }
    }
}