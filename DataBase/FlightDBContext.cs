using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Data
{
    public class FlightDBContext:DbContext
    {
        public FlightDBContext(DbContextOptions<FlightDBContext> options) : base(options)
        {

        }
        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScheduledDateModel> ScheduledDate { get; set; }
        public DbSet<TicketDetails> TicketDetails { get; set; }
        public DbSet<PassengerDetails> PassengerDetails { get; set; }


    }
}
