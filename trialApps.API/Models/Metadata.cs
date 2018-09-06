using System;

namespace trialApps.API.Models
{
    public class Metadata
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool isDelete { get; set; }
    }
}