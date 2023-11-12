namespace BetterMomshWebAPI.Models
{
    public class JournalModel
    {
        public string JournalName { get; set; }
        public string JournalEntry { get; set; }
        public byte[] PhotoData { get; set; }
        public long BookId { get; set; }
        public long weekId { get; set; }
    }
}
