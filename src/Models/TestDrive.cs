using System;

namespace CarShowRoom.Models
{
    public class TestDrive
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public Car Car { get; set; }

        public Client Client { get; set; }
    }
}