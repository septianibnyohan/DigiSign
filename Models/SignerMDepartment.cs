using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerMDepartment
    {
        public string Id { get; set; }
        public int? Company { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
