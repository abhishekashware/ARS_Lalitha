using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

using BCrypt.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Http;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    public class UserRegistrationController : ControllerBase
    {


        // private new_airlineEntities entity = new new_airlineEntities();

        public AppDbContext db { get; set; }

        public UserRegistrationController(AppDbContext _db)
        {
            db = _db;
        }



      /*  [HttpGet]

        public IEnumerable<User> Get()
        {
            List<User> s1 = db.Users.ToList();

            return s1;
        }

      */


        [HttpPost]
        public IActionResult signup([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }

            User user_obj = db.Users.Where(x => x.email == user.email).FirstOrDefault();

            if (user_obj == null)
            {
                db.Users.Add(user);

                db.SaveChanges();

                return Ok("Registration Successfull");
            }

            return BadRequest("Already exist");
        }



         /* [HttpPost]
          
         public IActionResult signup([FromBody]User user)
         {
             if (!ModelState.IsValid)
             {

                 return BadRequest(ModelState);

             }
             try
             {
                 User user_obj = db.Users.Where(x => x.email == user.email).FirstOrDefault();

                 if (user_obj == null)
                 {
                     user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                     db.Users.Add(user);
                     try
                     {
                         db.SaveChanges();
                     }
                     catch
                     {
                         return BadRequest("Not Found");
                     }

                     return Ok(user);
                 }

                 return BadRequest("Email-Id already Exists");
             }
             catch (Exception ex)
             {
                 

                 return BadRequest(ex.ToString());

             }*/


       


    }
}
