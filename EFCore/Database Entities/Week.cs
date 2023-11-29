using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("Weeks")]
    public class Week
    {
        [Key]
        public long weekId { get; set; }
        public string week_number { get; set; }

        [ForeignKey("MonthId")]
        public long MonthId { get; set; }
        [ForeignKey("BookId")]
        public long BookId { get; set; }

        [ForeignKey("user_id")]
        public Guid user_id { get; set; }
        public BabyBook babyBook { get; set; }
        public UserCredential user { get; set; }
        public Month mon { get; set; }

        public ICollection<Journal> journal { get; set; }
    }
}
