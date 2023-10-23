namespace BetterMomshWebAPI.Models
{
    public class BabyBookModel
    {
        public string Title { get; set; }
        public DateOnly Created { get; set; }
        public Guid user_id { get; set; }
    }
}
