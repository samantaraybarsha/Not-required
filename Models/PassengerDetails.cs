using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Models
{
    public class PassengerDetails
    {
        [Key]
        public int PassengerId { get; set; }
        public int TicketId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string SeatNumber { get; set; }
        public string Meal { get; set; }
    }
}
