using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Models
{
    public class FlightSearchDetailsDto
    {
        
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public int ScheduledDateId { get; set; }
        public string TripType { get; set; }
    }
}
