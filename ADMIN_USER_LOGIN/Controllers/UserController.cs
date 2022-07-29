using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {



        public AppDbContext db { get; set; }

        public UserController(AppDbContext _db)
        {
            db = _db;
        }


        //get user details by id
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.user_id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("User does not exist");
        }


        //register user
        [HttpPost]
        [Route("register")]
        public ActionResult Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }

            User user_obj = db.Users.FirstOrDefault(x => x.email == user.email);
            if (user_obj == null)
            {
                var result=db.Database.ExecuteSqlInterpolated($"exec dbo.SP_Register_User {user.title},{user.first_name}, {user.last_name}, {user.email}, {user.phone_number}, {user.dob},{user.password}");

                if (result != 0)
                    return Ok("Registration Successfull");

                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");

            }

            return BadRequest("Already exist");
        }

        //login user
        [HttpPost]
        [Route("login")]
        public ActionResult UserLogin([FromBody] Login u)
        {
            try
            {

                var user = db.Users.FirstOrDefault(x => x.email == u.email && x.password == u.password);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (user == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                return Ok("Valid");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        [HttpPost("flight/search")]
        public ActionResult<IEnumerable<SearchData>> SearchFlight([FromBody] SearchQuery query)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = db.FilteredFlights.FromSqlInterpolated($"exec dbo.SP_Search_Flight {query.booking_type}, {query.source_airport_id}, {query.destination_airport_id}, {query.departure_date}, {query.return_date}, {query.adults},{query.childs} , {query.infants}, {query.class_type}");
                if (result !=null)
                {
                    return Ok(result);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("flight/{id}")]
        public ActionResult<IEnumerable<Seat>> GetSeatsByFlightId(int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = db.GetSeatsByFId.FromSqlInterpolated($"exec dbo.SP_Get_Seats_By_FlightId {id}");
                if (result != null)
                {
                   return Ok(result);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        //[HttpPost("flight/book")]
        //public ActionResult<IEnumerable<SearchData>> BookFlight(SearchQuery query)
        //{

        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var result = db.FilteredFlights.FromSqlInterpolated($"exec dbo.SP_Search_Flight {query.booking_type}, {query.source_airport_id}, {query.destination_airport_id}, {query.departure_date}, {query.return_date}, {query.adults}, {query.infants}, {query.class_type}");
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Failed");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }

        //}
    }
}
