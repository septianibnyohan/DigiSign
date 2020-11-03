using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public int Company { get; set; }
        public string Role { get; set; }
        public string Permissions { get; set; }
        public string Signature { get; set; }
        public DateTime Lastnotification { get; set; }
        public string Hiddenfiles { get; set; }
        public string Timezone { get; set; }
        public string Lang { get; set; }
    }
}
