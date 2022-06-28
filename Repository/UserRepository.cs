using Flight_Booking_System.Data;
using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Repository
{
    public class UserRepository:IUserRepository
    {
        public FlightDBContext _context;
        public UserRepository(FlightDBContext context)
        {
            _context = context;
        }
        public void RegisterUser(string userName, byte[] passwordhash,byte[] passwordsalt,string Email)
        {

            User user = new User();
            user.UserName = userName;
            user.Passwordhash = passwordhash;
            user.Passwordsalt = passwordsalt;
            user.Email = Email;
            _context.Users.Add(user);
            _context.SaveChanges();

        }
        public User FetchRegisteredUserDetails(string userName)
        {
            return _context.Users.Where(x => x.UserName == userName).FirstOrDefault();

        }
    }
}
