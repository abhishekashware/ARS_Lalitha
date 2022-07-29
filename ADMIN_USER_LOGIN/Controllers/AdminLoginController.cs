using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Mvc;


namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    public class AdminLoginController : ControllerBase
    {
        //private new_airlineEntities db = new new_airlineEntities();

        public AppDbContext db { get; set; }

        public AdminLoginController(AppDbContext _db)
        {
            db = _db;
        }


        [HttpPost]

        public ActionResult adminloginvalid(Admin u)
        {
            try
            {
                var admin = db.Admins.Where(x => x.name == u.name && x.password == u.password).FirstOrDefault();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (admin != null)
                {
                    bool isValid = true;
                    if (!isValid)
                    {
                        return BadRequest("Invalid Credentials");
                    }

                }
                else
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
    }
}
