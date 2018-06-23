using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShowRoom.Models
{
    public class Order
    {
        public Order()
        {
            Date = DateTime.Now;
            Services = new List<ServiceOrderItem>();
            Parts = new List<PartOrderItem>();
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
            if ((Car != null || CarId !=0) && Parts != null && Services != null)
            {
                Price = IsSell ? Car.Price : 0 + Services.Sum(x => x.Service.Price) + Parts.Sum(x => x.PartType.Price);
            }
        }

        public bool IsPaid()
        {
            return Bills.Sum(x => x.Amount) >= Price;
        }

        public decimal NotPaid()
        {
            return Price - Bills.Sum(x => x.Amount);
        }

        public override string ToString()
        {
            return $"№ {Id} от {Date.ToShortDateString()}";
        }

        public string GetShortDescription()
        {
            var sb = new StringBuilder();
            if (IsSell)
                sb.Append($"Выдача автомобиля {Car.ToString()}, ");
            if (Services.Count > 0)
                sb.Append($"Оказание услуг: {String.Join(", ", Services.Select(x => x.Service.Name)) }. ");
            if (Parts.Count > 0)
                sb.Append($"Запчасти: {String.Join(", ", Parts.Select(x => x.PartType.Name)) }. ");
            return sb.ToString();
        }
    }
}