using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trialApps.API.Models
{
    public class User : Metadata
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string Intro { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }  
    }
}