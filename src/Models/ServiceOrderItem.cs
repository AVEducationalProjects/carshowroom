using System;

namespace CarShowRoom.Models
{
    public class ServiceOrderItem
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        public Service Service{ get; set; }
    }
}
