using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ADMIN_USER_LOGIN.Models
{
    public class Admin
    {
        [Key]
        public string admin_id { get; set; }

        [Required(ErrorMessage = " Name is required")]
        [Display(Name = "Name")]

        public string name { get; set; }


       

        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "Email Id")]
        [EmailAddress]

         public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 9 and 255 characters", MinimumLength = 9)]
        [DataType(DataType.Password)]

        public string password { get; set; }

    }
}
