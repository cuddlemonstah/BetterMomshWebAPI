using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("user_credential")]
    public class userCred
    {
        [Key]
        public long user_id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }
        // Navigation property to UserInfo
        public userInfo UserInfo { get; set; }
        public ICollection<BabyBook> BabyBooks { get; set; }
    }
}
