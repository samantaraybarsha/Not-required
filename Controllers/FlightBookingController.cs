using Flight_Booking_System.Models;
using Flight_Booking_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles= "Admin")]
    public class FlightBookingController : ControllerBase
    {
        private IFligthBookingRepo _flightRepo;
        public FlightBookingController(IFligthBookingRepo flightRepo)
        {
            _flightRepo = flightRepo;
        }
        [HttpPost]
        [Route("SearchFlights")]
        public List<FlightSearchDetails> SearchFlights([FromBody] FlightSearchDetailsDto flight)
        {
            return _flightRepo.SearchFlight(flight);

        }
        [HttpGet]
        [Route("BookFlight")]
        public string BookFlight(int flightId)
        {
            string userName = HttpContext.Session.GetString("UserName");
            return _flightRepo.BookFlight(flightId, userName);

        }
        [HttpPost]
        [Route("AddPassenger")]
        public IActionResult AddPassengerDetails(PassengerDetails passenger)
        {
            _flightRepo.AddPassengerDetails(passenger);
            return Ok("Passenger Added");
        }

        [HttpGet]
        [Route("BookedTicketDetails")]
        public TicketDetailsWithPassengers BookedTicketDetails(string PNR)
        {
            return _flightRepo.BookedTicketDetails(PNR);
        }

        [HttpGet]
        [Route("CancelTicket")]
        public IActionResult CancelTicket(string PNR)
        {
            _flightRepo.CancelTicket(PNR);
            return Ok("Ticket Cancelled");
        }
       
        [HttpGet]
        [Route("BookedTicketHistory")]
        public List<TicketDetailsWithPassengers> BookedTicketHistory(string EmailId)
        {
            return _flightRepo.BookedTicketHistory(EmailId);
        }
       
    }
}
