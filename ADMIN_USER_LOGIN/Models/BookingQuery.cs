using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    [Keyless]
    public class BookingQuery
    {

        [Required]
        public long user_id { get; set; }

        [Required]
        public long flight_id { get; set; }

        [Required]
        public string booking_type { get; set; }

        [Required]
        public Nullable<DateTime> return_date { get; set; }



        [Required]
        public string class_type { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> travel_date { get; set; }

        [Required]
        public string payment_mode { get; set; }


        [Required]
        public List<Passenger> passengers { get; set; }

    }
}
