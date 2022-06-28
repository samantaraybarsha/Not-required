using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Repository
{
    public interface IAdminRepository
    {
        public void AddFlight(FlightModel flight);
    }
}
