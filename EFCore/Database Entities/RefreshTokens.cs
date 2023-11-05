using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("RefreshTokens")]
    public class RefreshTokens
    {
        [Key]
        public Guid Id { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpired { get; set; }
        [ForeignKey("userCred")]
        public Guid user_id { get; set; }
        public userCred userCred { get; set; }
    }
}
