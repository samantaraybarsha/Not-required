using Flight_Booking_System.Data;
using Flight_Booking_System.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Flight_Booking_System.Repository
{
    public class FlightBookingRepo : IFligthBookingRepo
    {
        private FlightDBContext _context;
        public FlightBookingRepo(FlightDBContext context)
        {
            _context = context;
        }
        public List<FlightSearchDetails> SearchFlight(FlightSearchDetailsDto flight)
        {
            List<FlightModel> result;
            if (flight.TripType == "OneWay")
            {
                result = _context.Flights.Where(x => x.FromPlace == flight.FromPlace && x.ToPlace == flight.ToPlace).ToList();
            }
            else
            {
                result = _context.Flights.Where(x => (x.FromPlace == flight.FromPlace && x.ToPlace == flight.ToPlace) || x.FromPlace == flight.ToPlace && x.ToPlace == flight.FromPlace).ToList();
            }

            List<FlightSearchDetails> flights = new List<FlightSearchDetails>();

            foreach (var item in result)
            {
                FlightSearchDetails flightobj = new FlightSearchDetails();
                flightobj.FlightId = item.FlightId;

                flightobj.FlightNumber = item.FlightNumber;

                flightobj.FromPlace = item.FromPlace;
                flightobj.ToPlace = item.ToPlace;
                flightobj.DepartureTime = item.DepartureTime;
                flightobj.ArrivalTime = item.ArrivalTime;

                flightobj.ScheduledDateId = item.ScheduledDateId;
                flightobj.TotalCost = item.TotalCost;

                flights.Add(flightobj);
            }
            return flights;
        }

        public string BookFlight(int flightId,string username)
        {
            var UserName = username;
            FlightModel flight = _context.Flights.FirstOrDefault(x => x.FlightId == flightId);
            TicketDetails ticketDetails = new TicketDetails();
            ticketDetails.FlightId = flight.FlightId;
            ticketDetails.status = true;
            ticketDetails.userId = _context.Users.Where(x => x.UserName == UserName).FirstOrDefault().UserId;
            _context.TicketDetails.Add(ticketDetails);
            _context.SaveChanges();

            int bookedTicket = _context.TicketDetails.FirstOrDefault(x => x.FlightId == flightId).TicketId;
            string PNR = "PNR" + bookedTicket;
            _context.TicketDetails.Where(x => x.TicketId == bookedTicket).Update(x => new TicketDetails { PNR = PNR });
            return PNR;
        }
        public void AddPassengerDetails(PassengerDetails passenger)
        {
            _context.PassengerDetails.Add(passenger);
            _context.SaveChanges();
        }
        public TicketDetailsWithPassengers BookedTicketDetails(string PNR)
        {
            TicketDetailsWithPassengers ticketobj = new TicketDetailsWithPassengers();
            TicketDetails ticket = _context.TicketDetails.FirstOrDefault(x => x.PNR == PNR);
            int ticketId = _context.TicketDetails.FirstOrDefault(x => x.PNR == PNR).TicketId;
            var passengerdata = _context.PassengerDetails.Where(x => x.TicketId == ticketId).ToList();

            List<PassengerDetails> passengerList = new List<PassengerDetails>();
            foreach (var item in passengerdata)
            {
                PassengerDetails passengerobj = new PassengerDetails();
                passengerobj = item;
                passengerList.Add(passengerobj);

            }
            ticketobj.passengers = passengerList;
            ticketobj.ticket = ticket;
            return ticketobj;
        }

        public void CancelTicket(string PNR)
        {
            TicketDetails ticket = _context.TicketDetails.FirstOrDefault(x => x.PNR == PNR);
            _context.TicketDetails.Where(x => x.PNR == PNR).Update(x => new TicketDetails { status = false });
        }

        public List<TicketDetailsWithPassengers> BookedTicketHistory(string Email)
        {
            List<TicketDetailsWithPassengers> allTicketHistort = new List<TicketDetailsWithPassengers>();
            TicketDetailsWithPassengers ticketobj = new TicketDetailsWithPassengers();
            int userId = _context.Users.FirstOrDefault(x => x.Email == Email).UserId;
            List<TicketDetails> ticket = _context.TicketDetails.Where(x => x.userId == userId).ToList();
            foreach (var tick in ticket)
            {
                var passengerdata = _context.PassengerDetails.Where(x => x.TicketId == tick.TicketId).ToList();

                List<PassengerDetails> passengerList = new List<PassengerDetails>();
                foreach (var item in passengerdata)
                {
                    PassengerDetails passengerobj = new PassengerDetails();
                    passengerobj = item;
                    passengerList.Add(passengerobj);

                }
                ticketobj.passengers = passengerList;
                ticketobj.ticket = tick;
                allTicketHistort.Add(ticketobj);
            }

            return allTicketHistort;
        }
    }
    }
