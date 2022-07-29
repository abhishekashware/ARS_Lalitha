using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    public class ResetPasswordController : Controller
    {
        public AppDbContext db { get; set; }

        public ResetPasswordController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpPost]
        public ActionResult resetpassword(Login u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = db.Users.Where(x => x.email == u.email).FirstOrDefault();
                if (user != null)
                {
                    var userobj = db.user_otp.Where(x => x.user_id == user.user_id).FirstOrDefault();
                    if (userobj == null)
                    {
                        return BadRequest("NOT ALLOWED");
                    }
                    user.password = u.password;
                    db.Entry(user).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                        return NotFound();
                    }

                    return Ok("Password Changed");

                }
                else
                {
                    return BadRequest("Wrong Email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

    }
}
