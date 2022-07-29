using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ADMIN_USER_LOGIN.Models
{
    public class User
    {
        public long user_id { get; set; }


        [Key]
        [Required(ErrorMessage = "Email Id is required")]
        // [Display(Name = "Email Id")]
        [EmailAddress]
        public string email { get; set; }


        [Required(ErrorMessage = "Title is required")]
        //[Display(Name = "Title")]
        public string title { get; set; }

       

        [Required(ErrorMessage = "First Name is required")]
       // [Display(Name = "First Name")]
        public string first_name { get; set; }

      
        [Required(ErrorMessage = "Last Name is required")]
        //[Display(Name = "Last Name")]
        public string last_name { get; set; }


        // [Required(ErrorMessage = "Phone Number is required")]
        // [Phone]
         public string phone_number { get; set; }


         [Required(ErrorMessage = "Date of Birth Id is required")]
         [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public Nullable<DateTime> dob { get; set; }
         

         [Required(ErrorMessage = "Password is required")]
         [DataType(DataType.Password)]
         public string password { get; set; }



        // For password validation => " ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,}$ "



        /*[Required(ErrorMessage = "Confirm Password is required")]
         [DataType(DataType.Password)]
         [Compare("Password")]
         public string ConfirmPassword { get; set; }   */


    }
}
