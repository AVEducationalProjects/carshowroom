using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Stage Stage { get; set; }

        public ICollection<Car> Cars { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}