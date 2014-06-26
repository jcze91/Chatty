using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class UserModel
    {
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        [JsonIgnore]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        [JsonIgnore]
        public string ConfirmNewPassword { get; set; }
        [JsonProperty("userName")]
        public string Username { get; set; }
        [JsonProperty("lastName")]
        public string Lastname { get; set; }
        [JsonProperty("firstName")]
        public string Firstname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("isEnable")]
        public bool isEnable { get; set; }
        [JsonProperty("id")]
        public int Id;
        [JsonIgnore]
        public string Token;
    }
}