using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("user_profile")]
    public class userInfo
    {
        [Key]
        [ForeignKey("userCred")]
        public long user_id { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string MiddleName { get; set; }

        public DateTime Birthdate { get; set; }

        [MaxLength(255)]
        public string Religion { get; set; }

        [MaxLength(255)]
        public string Occupation { get; set; }

        [MaxLength(50)]
        public string RelationshipStatus { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "numeric(12,2)")]
        public decimal ContactNumber { get; set; }

        public userCred userCred { get; set; }

    }
}
