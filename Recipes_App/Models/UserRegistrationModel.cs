using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Recipes_App.Models
{
    public class UserRegistrationModel
    {
        [Key]
        public int pkid { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username cannot be blank.")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string User_Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password cannot be blank.")]
        [DataType(DataType.Password)]
        public string User_Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password cannot be blank.")]
        [DataType(DataType.Password)]
        [Compare("User_Password")]
        public string Confirm_Password { get; set; }
    }
}
