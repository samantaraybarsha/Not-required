using Flight_Booking_System.Data;
using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Repository
{
    public class AdminRepository:IAdminRepository
    {
        private readonly FlightDBContext _context;

        public AdminRepository(FlightDBContext context)
        {
            _context = context;
        }
        public void AddFlight(FlightModel flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }
    }
}
