using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class ViewWorkflow
    {
        public int? RequestId { get; set; }
        public string RequestorEmail { get; set; }
        public string RequestorNote { get; set; }
        public int? EmployeeId { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int WorkflowId { get; set; }
        public string Filename { get; set; }
        public string EmployeeName { get; set; }
        public string LanId { get; set; }
    }
}
