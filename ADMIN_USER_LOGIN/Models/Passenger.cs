using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    [Keyless]
    public class Passenger
    {
        [Required]
        public string name { get; set; }

        [Required]

        public string email { get; set; }

        [Required]

        public string phone_no { get; set; }

        [Required]

        public int age { get; set; }

        [Required]

        public string gender { get; set; }

        [Required]

        public long seat_id { get; set; }

    }
}
