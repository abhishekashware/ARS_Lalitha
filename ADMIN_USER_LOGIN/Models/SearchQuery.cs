using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    public class SearchQuery
    {

        [Required]
        public string booking_type { get; set; } //one_way or return

        [Required]

        public long source_airport_id { get; set; }

        [Required]

        public long destination_airport_id { get; set; }

        [Required]

        public Nullable<DateTime> departure_date { get; set; }

        
        public Nullable<DateTime> return_date { get; set; }

        [Required]

        public int adults { get; set; }

        [Required]

        public int childs { get; set; }

        [Required]

        public int infants { get; set; }

        [Required]

        public string class_type { get; set; }
    }
}
