namespace DigiSign.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class signer_file
    {
        [Key]
        public int id { get; set; }
        public int request_id { get; set; }
        public int category_id { get; set; }
        public string name { get; set; }
        public string filename { get; set; }
        public string extension { get; set; }
        public Nullable<int> size { get; set; }
        public string document_key { get; set; }
        public string status { get; set; }
        public string editted { get; set; }
        public string is_template { get; set; }
        public string template_fields { get; set; }
        public string sign_reason { get; set; }
        public string accessibility { get; set; }
        public string public_permissions { get; set; }
        public Nullable<int> company { get; set; }
        public Nullable<int> uploaded_by { get; set; }
        public Nullable<System.DateTime> uploaded_on { get; set; }
        public string guid { get; set; }
    }
}
