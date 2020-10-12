using System.ComponentModel.DataAnnotations;

namespace DigiSign.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class signer_files_share
    {
        [Key]
        public int share_id { get; set; }
        public int request_id { get; set; }
        public string employee_id { get; set; }
        public string employee_email { get; set; }
        public string rules { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_time { get; set; }
        public string shared_key { get; set; }
    }
}
