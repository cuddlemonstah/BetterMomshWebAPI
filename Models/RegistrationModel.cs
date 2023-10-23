namespace BetterMomshWebAPI.Models
{
    public class RegistrationModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Religion { get; set; }

        public string Occupation { get; set; }

        public string RelationshipStatus { get; set; }

        public string Address { get; set; }

        public decimal? ContactNumber { get; set; }
        public string? Token { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Expires { get; set; }
    }
}
