using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterMomshWebAPI.EFCore
{
    [Table("Journals")]
    public class Journal
    {
        [Key]
        public long journalId { get; set; }
        public string JournalName { get; set; }
        public DateTime Entry_Date { get; set; }
        [DataType(DataType.Text)]
        [MaxLength]
        public string journalEntry { get; set; }
        public byte[] PhotoData { get; set; }

        [ForeignKey("BookId")]
        public long BookId { get; set; }

        [ForeignKey("user_id")]
        public Guid user_id { get; set; }

        [ForeignKey("weekId")]
        public long weekId { get; set; }

        public BabyBook babyBook { get; set; }
        public UserCredential user { get; set; }
        public Week week { get; set; }
    }
}
