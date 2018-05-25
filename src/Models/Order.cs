using System;
using System.Collections.Generic;

namespace CarShowRoom.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Client Client { get; set; }
        
        public Car Car { get; set; }

        public IList<Service> Services { get; set; }

        public IList<PartOrderItem> Parts { get; set; }
    }
}