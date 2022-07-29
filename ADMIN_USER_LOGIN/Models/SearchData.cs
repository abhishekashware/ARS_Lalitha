using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    [Keyless]
    public class SearchData
    {

  

        public long flight_id { get; set; } 


        public string source_city { get; set; }

        public string destination_city { get; set; }

        public string flight_name { get; set; }


        public DateTime departure_time { get; set; }

        public DateTime arrival_time { get; set; }

    }
}
