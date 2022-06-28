using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Flight_Booking_System.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Flight_Booking_System.Repository;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public IUserRepository _user;
        public AuthenticationController(IConfiguration configuration, IUserRepository user)
        {
            Configuration = configuration;
            _user = user;
        }

        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] UserDto request)
        {
            Createpasswordhash(request.Password, out byte[] passwordhash, out byte[] passwordsalt);

            _user.RegisterUser(request.UserName, passwordhash, passwordsalt,request.Email);
            return Ok(_user);

        }

        private void Createpasswordhash(string Password, out byte[] passwordhash, out byte[] passwordsalt)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }

        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLogin request)
        {
            User user= _user.FetchRegisteredUserDetails(request.UserName);
            if (String.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("User does not exist");
            }
            if (!verifyPassword(request.Password, user.Passwordhash, user.Passwordsalt))
            {
                return BadRequest("Wrong Password");
            }
            string token = CreateToken(user);
            HttpContext.Session.SetString("UserName", request.UserName);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Token"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool verifyPassword(string password, byte[] Passwordhash, byte[] Passwordsalt)
        {
            using (var hmac = new HMACSHA512(Passwordsalt))
            {
                var computehash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computehash.SequenceEqual(Passwordhash);
            }
        }

    }
}
