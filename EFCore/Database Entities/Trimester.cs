using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("Trimester")]
    public class Trimester
    {
        [Key]
        public long TrimesterId { get; set; }
        public string Trimesters { get; set; }
        [ForeignKey("BookId")]
        public long BookId { get; set; }
        public BabyBook babyBook { get; set; }
        public IEnumerable<Months> mon { get; set; }
    }
}
