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
            try
            {
                var user = db.Users.FirstOrDefault(u => u.user_id == id);
                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest("User does not exist");
            }
            catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }


        //register user
        [HttpPost]
        [Route("register")]
        public ActionResult Post([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);

                }

                User user_obj = db.Users.FirstOrDefault(x => x.email == user.email);
                if (user_obj == null)
                {
                    var result = db.Database.ExecuteSqlInterpolated($"exec dbo.SP_Register_User {user.title},{user.first_name}, {user.last_name}, {user.email}, {user.phone_number}, {user.dob},{user.password}");

                    if (result != 0)
                        return Ok("Registration Successfull");

                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed");

                }

                return BadRequest("Already exist");
            }catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
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



        //search flight
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


        //get seats in a flight  by flight id
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


        //get all the airports available
        [HttpGet]
        [Route("airports")]

        public ActionResult GetAirports()
        {

            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);

                }
                var result = db.Airports.ToList();

                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");
            }catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }


        //change password
        [HttpPost]
        [Route("changepassword")]
        public ActionResult ChangePassword([FromBody]ChangePasswordModel model)
        {
            try
            {
                User u = db.Users.FirstOrDefault(user => user.email == model.email && user.password==model.old_password);
                if (u == null)
                {
                    return BadRequest("Invalid User id");
                }
                var res = db.Database.ExecuteSqlInterpolated($"exec dbo.SP_Change_Password {u.user_id}, {model.new_password}");
                if (res != 0)
                {
                    return Ok("Password Updated Successfully");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");


            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        //book flight
        [HttpPost]
        [Route("book")]
        public ActionResult BookFlight([FromBody] BookingQuery query)
        {

            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);

                }

                Flight flight = db.Flights.FirstOrDefault(f => f.flight_id == query.flight_id);
                decimal amount;
                if (flight == null)
                {
                    return BadRequest("Flight does not exist");
                }

                if (query.class_type == "business")
                {
                    amount = flight.business_fare;
                }
                else if (query.class_type == "economic")
                {
                    amount = flight.economic_fare;
                }
                else
                {
                    return BadRequest("Invalid Flight Class Type");
                }
                if (query.booking_type == "one_way" || query.booking_type == "return") { }
                else
                {
                    return BadRequest("Invalid booking type");
                }
                User u = db.Users.FirstOrDefault(user => user.user_id == query.user_id);
                if (u == null)
                {
                    return BadRequest("Invalid User");

                }

                if (query.payment_mode == "credit_card" || query.payment_mode == "debit_card") { }
                else
                {
                    return BadRequest("Invalid Payment mode");
                }
                List<BookingData> result = db.GetBookingData.FromSqlInterpolated($"exec dbo.SP_Book_Flight {query.user_id},{query.flight_id}, {query.booking_type}, {query.return_date}, {query.passengers.FindAll(p => p.age > 2).Count * amount},{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}, {query.class_type}, {flight.departure_time}, {query.payment_mode}, {query.passengers.Count}").ToList();

                if (result !=null && result.Count>0)
                {
                    List<Seat> seats = db.GetSeatsByFId.FromSqlInterpolated($"exec dbo.SP_Get_Seats_By_FlightId {flight.flight_id}").ToList();

                    for (int i = 0; i < query.passengers.Count; i++)
                    {
                        //check seat availibility
                        Seat s = seats.FirstOrDefault(seat => seat.seat_id == query.passengers[i].seat_id);
                        if (s == null)
                        {
                            return BadRequest("Invalid Seat No.");
                        }
                        if (s.is_booked == true)
                        {
                            return BadRequest($"Seat {s.seat_name} ({s.seat_id}) is already booked");

                        }
                        var passenger = db.Database.ExecuteSqlInterpolated($"exec dbo.SP_Add_Passengers {query.passengers[i].name},{query.passengers[i].email}, {result[0].booking_id}, {query.passengers[i].phone_no}, {query.passengers[i].age}, {query.passengers[i].gender},{query.passengers[i].seat_id}");
                        if (passenger == 0)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add passengers");

                        }
                    }


                    return Ok("Flight Booked Successfully");

                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to book");
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
