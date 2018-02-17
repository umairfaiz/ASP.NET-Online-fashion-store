using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CorrectTemp.Models
{
    public class userAccount
    {
        [Key]//annotations
        public int userID { get; set; }

        [Required(ErrorMessage ="Enter Username.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter shipping address.")]
        public string shippingAddress { get; set; }

        [Required(ErrorMessage = "Enter credit card number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Enter email address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [Compare("Password", ErrorMessage ="Please confirm your password.")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        public string userType { get; set; }


    }
}