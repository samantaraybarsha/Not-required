using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Models
{
    public class FlightSearchDetails
    {
        [Key]
        public int FlightId { get; set; }

        public string FlightNumber { get; set; }
        
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public int ScheduledDateId { get; set; }
        public decimal TotalCost { get; set; }
        public string TripType { get; set; }
    }
}
