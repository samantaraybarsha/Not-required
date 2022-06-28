using Flight_Booking_System.Models;
using Flight_Booking_System.Repository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepo = null;

        public AdminController(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

        [HttpPost]
        [Route("AddFlight")]
        public IActionResult AddFlight([FromBody] FlightModel value)
        {
            if (value?.FlightId == null)
            {
                return new BadRequestResult();
            }
            else
            {
                _adminRepo.AddFlight(value);
            }
            return Created(value.FlightId.ToString(), value.FlightNumber);
        }

    }
}
