using System;

namespace CarShowRoom.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public Order Order { get; set; }

        public int OrderId { get; set; }
    }
}