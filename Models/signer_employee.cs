using System.ComponentModel.DataAnnotations;

namespace DigiSign.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class signer_employee
    {
        [Key]
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string employee_email { get; set; }
        public string superior_id { get; set; }
        public string lan_id { get; set; }
        public string department_id { get; set; }
        public Nullable<System.DateTime> lastnotification { get; set; }
    }
}
