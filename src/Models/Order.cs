using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShowRoom.Models
{
    public class Order
    {
        public Order()
        {
            Date = DateTime.Now;
        }
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Client Client { get; set; }

        public decimal Price { get; set; }

        public bool IsSell { get; set; }

        public bool Done { get; set; }

        public Car Car { get; set; }

        public IList<ServiceOrderItem> Services { get; set; }

        public IList<PartOrderItem> Parts { get; set; }

        public IList<Bill> Bills { get; set; }

        public int CarId { get; set; }

        public int ClientId { get; set; }

        public void UpdatePrice()
        {
            if (Car != null && Parts != null && Services != null)
            {
                Price = IsSell ? Car.Price : 0 + Services.Sum(x => x.Service.Price) + Parts.Sum(x => x.PartType.Price);
            }
        }
    }
}