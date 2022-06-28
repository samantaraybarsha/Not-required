using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Models
{
    public class ScheduledDateModel
    {
        [Key]
        public int ScheduledDateId { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
