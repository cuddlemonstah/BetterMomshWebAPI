using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BetterMomshWebAPI.EFCore
{
    [Table("BabyBook")]
    public class BabyBook
    {
        [Key]
        public long BookId { get; set; }
        public string Title { get; set; }
        public DateOnly Created { get; set; }

        [ForeignKey("userCred")]
        public Guid user_id { get; set; }

        public userCred UserCred { get; set; }

        public ICollection<Trimester> trim { get; set; }
        public ICollection<Journal> journal { get; set; }
    }
}
