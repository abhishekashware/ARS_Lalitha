using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    public class ValidateOTPController : Controller
    {

        public AppDbContext db { get; set; }

        public ValidateOTPController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpPost]
        public ActionResult validateOTP(ValidateOTP otp_obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = db.Users.Where(x => x.email == otp_obj.email).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("Email ID not Registered");
                }
                var userobj = db.user_otp.Where(x => x.user_id == user.user_id).FirstOrDefault();
                if (userobj.otp.ToString() == otp_obj.otp.ToString())
                {
                    return Ok("verified");
                }
                return BadRequest("OTP not Valid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
