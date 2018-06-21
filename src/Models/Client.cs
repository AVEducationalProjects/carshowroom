using System;
using System.Collections.Generic;

namespace CarShowRoom.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Stage Stage { get; set; }

        public ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}