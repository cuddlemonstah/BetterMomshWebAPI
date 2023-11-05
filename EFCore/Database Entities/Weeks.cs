using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("Weeks")]
    public class Weeks
    {
        [Key]
        public long weekId { get; set; }
        public string week_number { get; set; }

        [ForeignKey("MonthId")]
        public long MonthId { get; set; }

        public Months mon { get; set; }

        public ICollection<Journal> journal { get; set; }
    }
}
