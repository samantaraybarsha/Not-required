using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Booking_System.Repository
{
    public interface IUserRepository
    {
        public void RegisterUser(string password, byte[] passwordhash, byte[] passwordsalt,string email);
        public User FetchRegisteredUserDetails(string userName);
    }
}
