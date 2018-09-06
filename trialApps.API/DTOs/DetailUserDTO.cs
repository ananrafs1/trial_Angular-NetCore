using System;
using System.Collections.Generic;
using trialApps.API.Models;

namespace trialApps.API.DTOs
{
    public class DetailUserDTO : ListUsersDTO
    {
        public string Intro { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public ICollection<DetailsUserPhotoDTO> Photos { get; set; }
    }
}