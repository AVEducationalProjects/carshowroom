using System;

namespace CarShowRoom.Models
{
    public class TestDrive
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public bool Complete { get; set; }

        public Car Car { get; set; }

        public Client Client { get; set; }

        public int ClientId { get; set; }

        public int CarId { get; set; }
    }
}