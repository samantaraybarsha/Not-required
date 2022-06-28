using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Models
{
    public class TicketDetails
    {
        [Key]
        public int TicketId { get; set; } 
        public int userId { get; set; }
        public int FlightId { get; set; }
        public string PNR { get; set; }
        public bool status { get; set; } 
    }
}
