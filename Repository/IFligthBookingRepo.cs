using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Repository
{
    public interface IFligthBookingRepo
    {
        public List<FlightSearchDetails> SearchFlight(FlightSearchDetailsDto flight);
        public string BookFlight(int flightId,string userName);
        public void AddPassengerDetails(PassengerDetails passenger);
        public TicketDetailsWithPassengers BookedTicketDetails(string PNR);
        public void CancelTicket(string PNR);

        public List<TicketDetailsWithPassengers> BookedTicketHistory(string Email);
    }
}
