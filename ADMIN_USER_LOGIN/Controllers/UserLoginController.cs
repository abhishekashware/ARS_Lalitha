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

    public class UserLoginController : Controller
    {

        //private new_airlineEntities entity = new new_airlineEntities();

        public AppDbContext db { get; set; }

        public UserLoginController(AppDbContext _db)
        {
            db = _db;
        }



        [HttpPost]
        public ActionResult userloginvalid(Login u)
        {
            try
            {

                var user = db.Users.Where(x => (string.Concat(x.first_name, x.last_name).Equals(u.username)) && (x.password == u.password)).FirstOrDefault();

                if (!ModelState.IsValid)
                 {
                    return BadRequest(ModelState);
                 }
                 if (user != null)
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
    
