using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public string? RefreshTokens { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpired { get; set; }
        [ForeignKey("userCred")]
        public Guid user_id { get; set; }
        public UserCredential userCred { get; set; }
    }
}
