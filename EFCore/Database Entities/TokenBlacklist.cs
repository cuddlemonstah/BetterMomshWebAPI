using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("TokenBlacklist")]
    public class TokenBlacklist
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
