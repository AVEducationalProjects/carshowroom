using System;
using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Order Order { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}