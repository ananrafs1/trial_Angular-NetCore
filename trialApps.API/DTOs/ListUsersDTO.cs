using System;
using System.Collections.Generic;
using trialApps.API.Models;

namespace trialApps.API.DTOs
{
    public class ListUsersDTO : Metadata
    {
         public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoURL { get; set; }  
    }
}