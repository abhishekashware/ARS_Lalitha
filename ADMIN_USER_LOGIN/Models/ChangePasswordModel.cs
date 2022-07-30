using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN_USER_LOGIN.Models
{
    public class ChangePasswordModel
    {
        [Key]
        [Required]
        public string email { get; set; }

        [Required]

        [DataType(DataType.Password)]
        public string old_password { get; set; }

        [Required]

        [DataType(DataType.Password)]

        public string new_password { get; set; }
    }
}
