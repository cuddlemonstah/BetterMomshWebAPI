using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("user_credential")]
    public class UserCredential
    {
        [Key]
        public Guid user_id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }
        // Navigation property to UserInfo
        [Required]
        public string Role { get; set; }
        public UserInformation UserInfo { get; set; }
        public RefreshToken RefreshTokens { get; set; }
        public ICollection<BabyBook> BabyBooks { get; set; }
        public ICollection<Trimester> Trimester { get; set; }
        public ICollection<Month> Month { get; set; }
        public ICollection<Week> Week { get; set; }
        public ICollection<Journal> Journal { get; set; }
    }
}
