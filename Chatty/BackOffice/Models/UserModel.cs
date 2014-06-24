using System;
using System.ComponentModel.DataAnnotations;

namespace Chatty.BackOffice.Models
{
    public class UserModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public bool isEnable { get; set; }
        public int Id;
    }
}