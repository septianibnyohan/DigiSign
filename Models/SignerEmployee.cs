using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerEmployee
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string SuperiorId { get; set; }
        public string LanId { get; set; }
        public string DepartmentId { get; set; }
        public DateTime? Lastnotification { get; set; }
    }
}
