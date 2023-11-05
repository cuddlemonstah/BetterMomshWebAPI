using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("Months")]
    public class Months
    {
        [Key]
        public long MonthId { get; set; }
        public string Month { get; set; }
        [ForeignKey("TrimesterId")]
        public long TrimesterId { get; set; }

        public Trimester trim { get; set; }
        public ICollection<Weeks> weeks { get; set; }

    }
}
