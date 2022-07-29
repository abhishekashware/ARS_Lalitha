using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    public class Seat
    {
        [Key]
        [Required]
        public long seat_id { get; set; }

        [Required]
        public string seat_name { get; set; }


        [Required]
        public long flight_id { get; set; }

        [Required]
        public int row_no { get; set; }

        [Required]
        public int col_no { get; set; }

        [Required]
        public string seat_type { get; set; }

        [Required]
        public bool is_booked { get; set; }

    }
}
