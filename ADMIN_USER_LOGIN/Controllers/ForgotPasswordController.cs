

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ADMIN_USER_LOGIN.Controllers
{
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        public AppDbContext db { get; set; }

        public ForgotPasswordController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpPost]

        public ActionResult forgotpassword(ForgotPassword forgotpassword)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                

                var user = db.Users.Where(x => x.email == forgotpassword.email).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("user doesnot exist");
                }

                Random random_number = new Random();
                int OTP = random_number.Next(1000, 9999);

               var user_in_otp = db.validateOTPs.Where(x => x.email == user.email).FirstOrDefault();

                if (user_in_otp != null)
                {
                    db.validateOTPs.Remove(user_in_otp);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.ToString());
                    }

                }

      
                User_OTP userobj = new User_OTP();
                userobj.user_id = user.user_id;
                userobj.otp = OTP;
                db.user_otp.Add(userobj);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edsins007@gmail.com");
                mail.To.Add(forgotpassword.email);
                mail.Subject = "Airlines OTP";
                mail.Body = "OTP is " + OTP.ToString();

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("edsins007@gmail.com", "radioactive1!");    //In parameters mail_id and password
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return Ok("Mail Sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        

    }


}

