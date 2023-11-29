using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("Months")]
    public class Month
    {
        [Key]
        public long MonthId { get; set; }
        public string Months { get; set; }
        [ForeignKey("TrimesterId")]
        public long TrimesterId { get; set; }
        [ForeignKey("BookId")]
        public long BookId { get; set; }
        [ForeignKey("user_id")]
        public Guid user_id { get; set; }
        public BabyBook babyBook { get; set; }
        public Trimester trim { get; set; }
        public UserCredential user { get; set; }
        public ICollection<Week> weeks { get; set; }

    }
}
