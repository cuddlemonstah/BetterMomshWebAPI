using System.ComponentModel.DataAnnotations;

namespace BetterMomshWebAPI.Models.JWT_Models
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
