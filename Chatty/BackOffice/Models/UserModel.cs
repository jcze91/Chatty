using System;
using System.ComponentModel.DataAnnotations;

namespace Chatty.BackOffice.Models
{
    public class UserModel
    {
        public string Pseudo { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public Guid? Uid { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public bool IsBanned { get; set; }
    }
}