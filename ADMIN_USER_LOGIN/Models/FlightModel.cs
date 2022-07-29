using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    public class FlightModel
    {
        [Required]
        public string flight_name { get; set; }

        [Required]

        public Nullable<DateTime> departure_time { get; set; }

        [Required]

        public Nullable<DateTime> arrival_time { get; set; }


        [Required]
        public decimal economic_fare { get; set; }


        [Required]
        public decimal business_fare { get; set; }


        [Required]
        public long source_airport_id { get; set; }

        [Required]
        public long destination_airport_id { get; set; }

    }
}
