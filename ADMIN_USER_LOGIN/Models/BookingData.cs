using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    [Keyless]
    public class BookingData
    {
        [Required]
        public long booking_id { get; set; }

        [Required]
        public long flight_id { get; set; }

    }
}
