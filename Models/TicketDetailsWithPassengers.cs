using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Models
{
    public class TicketDetailsWithPassengers
    {
        public TicketDetails ticket { get; set; }
        public List<PassengerDetails> passengers { get; set; }
    }
}
