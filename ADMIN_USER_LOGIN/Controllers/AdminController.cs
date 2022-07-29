using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        //private new_airlineEntities db = new new_airlineEntities();

        public AppDbContext db { get; set; }

        public AdminController(AppDbContext _db)
        {
            db = _db;
        }


        //post add flight
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] Login u)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var admin = db.Admins.FirstOrDefault(x => x.email == u.email && x.password == u.password);

                if (admin == null)
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


        //------------------------ FLIGHTS---------------------

        [HttpGet]
        [Route("flights")]
        public ActionResult GetFlights()
        {
            try
            {
                List<Flight> flights = db.Flights.ToList();

                return Ok(flights);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [Route("addflight")]
        public ActionResult AddFlight([FromBody] FlightModel f)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (f.departure_time > f.arrival_time)
                {
                    return BadRequest("Dept. Time is later than Arrival Time.");
                }



                var result = db.Database.ExecuteSqlInterpolated($"exec dbo.SP_Add_Flight {f.flight_name}, {f.departure_time}, {f.arrival_time}, {f.economic_fare}, {f.business_fare}, {f.source_airport_id}, {f.destination_airport_id}");
                if (result != 0)
                {
                    return Ok("Flight Added Successfully");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpDelete]
        [Route("flight/{name}")]
        public ActionResult DeleteFlight(string name)
        {

            try {
                var result = db.Database.ExecuteSqlInterpolated($"exec dbo.SP_Delete_Flight {name}");
                if (result != 0)
                {
                    return Ok("Flight Deleted Successfully");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        


    }
}
