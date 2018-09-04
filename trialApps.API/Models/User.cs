using System.ComponentModel.DataAnnotations;

namespace trialApps.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] PassSalt { get; set; }     
    }
}