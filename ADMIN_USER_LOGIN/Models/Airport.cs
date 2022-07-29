using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    public class Airport

    {

        [Required]
        [Key]
        public long airport_id { get; set; }
        
        [Required]
        public string city { get; set; }

        [Required]
        public string state { get; set; }

        [Required]
        public string country { get; set; }


    }
}
