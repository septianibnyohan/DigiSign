using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class SignerFile
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public int? Size { get; set; }
        public string DocumentKey { get; set; }
        public string Status { get; set; }
        public string Editted { get; set; }
        public string IsTemplate { get; set; }
        public string TemplateFields { get; set; }
        public string SignReason { get; set; }
        public string Accessibility { get; set; }
        public string PublicPermissions { get; set; }
        public int? Company { get; set; }
        public int? UploadedBy { get; set; }
        public DateTime? UploadedOn { get; set; }
        public string Guid { get; set; }
    }
}
