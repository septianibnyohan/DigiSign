using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class ViewWorkflowById
    {
        public int RequestId { get; set; }
        public string RequestorEmail { get; set; }
        public string RequestorNote { get; set; }
        public string Filename { get; set; }
    }
}
