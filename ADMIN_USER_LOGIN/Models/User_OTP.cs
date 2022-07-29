using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ADMIN_USER_LOGIN.Models
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class User_OTP
    {
        [Key]
        public int user_id { get; set; }

        public int otp { get; set; }
    }
}
