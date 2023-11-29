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
        public Guid user_Id { get; set; }

        public UserCredential UserCred { get; set; }

        public ICollection<Trimester> Trimesters { get; set; }
        public ICollection<Month> Month { get; set; }
        public ICollection<Week> Week { get; set; }
        public ICollection<Journal> Journals { get; set; }
    }
}
