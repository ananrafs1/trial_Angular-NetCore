using System.ComponentModel.DataAnnotations;

namespace trialApps.API.Models
{
    public class Value
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}