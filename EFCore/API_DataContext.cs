using Microsoft.EntityFrameworkCore;

namespace BetterMomshWebAPI.EFCore
{
    public class API_DataContext : DbContext
    {
        public API_DataContext(DbContextOptions<API_DataContext> options)
            : base(options)
        {
        }
        public DbSet<UserCredential>? UserCred { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }
        public DbSet<UserInformation> UserInfo { get; set; }
        public DbSet<BabyBook> BabyBook { get; set; }
        public DbSet<Trimester> Trimester { get; set; }
        public DbSet<Month> Month { get; set; }
        public DbSet<Journal> Journal { get; set; }
        public DbSet<Week> Week { get; set; }
        public DbSet<TokenBlacklist> BlackList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<TokenBlacklist>()
                .HasKey(x => x.Id);
            // One-to-One Relationship Between UserCred and UserInfo
            modelBuilder.Entity<UserCredential>()
                .HasOne(uc => uc.UserInfo)
                .WithOne(ui => ui.userCred) // Assuming UserInfo has a reference to UserCred
                .HasForeignKey<UserInformation>(ui => ui.user_id);

            modelBuilder.Entity<UserCredential>()
                .HasOne(uc => uc.RefreshTokens)
                .WithOne(ui => ui.userCred) // Assuming RefreshToken has a reference to UserCred
                .HasForeignKey<RefreshToken>(ui => ui.user_id);

            // One-to-Many Relationship Between UserCred and BabyBook
            modelBuilder.Entity<BabyBook>()
                .HasOne(b => b.UserCred)
                .WithMany(uc => uc.BabyBooks) // Assuming UserCred has a collection of BabyBooks 
                .HasForeignKey(b => b.user_Id);

            // One-to-Many Relationship Between BabyBook and Trimester
            modelBuilder.Entity<Trimester>()
                .HasOne(t => t.babyBook)
                .WithMany(b => b.Trimesters) // Assuming BabyBooks has a collection of Trimesters
                .HasForeignKey(t => t.BookId);
            // One-to-Many Relationship Between UserCred and Trimester
            modelBuilder.Entity<Trimester>()
                .HasOne(t => t.user)
                .WithMany(b => b.Trimester) // Assuming UserCred has a collection of Trimesters
                .HasForeignKey(t => t.user_id);

            // One-to-Many Relationship Between Trimester and Months
            modelBuilder.Entity<Month>()
                .HasOne(t => t.trim)
                .WithMany(b => b.mon) // Assuming Trimesters has a collection of Months
                .HasForeignKey(t => t.TrimesterId);

            // One-to-Many Relationship Between Babybook and Months
            modelBuilder.Entity<Month>()
                .HasOne(t => t.babyBook)
                .WithMany(b => b.Month) // Assuming Babybook has a collection of Months
                .HasForeignKey(t => t.BookId);

            // One-to-Many Relationship Between User and Months
            modelBuilder.Entity<Month>()
                .HasOne(t => t.user)
                .WithMany(b => b.Month) // Assuming user has a collection of Months
                .HasForeignKey(t => t.user_id);

            // One-to-Many Relationship Between Month and Weeks
            modelBuilder.Entity<Week>()
                .HasOne(t => t.mon)
                .WithMany(b => b.weeks) // Assuming Months has a collection of Weeks
                .HasForeignKey(t => t.MonthId);
            // One-to-Many Relationship Between babybook and Weeks
            modelBuilder.Entity<Week>()
                .HasOne(t => t.babyBook)
                .WithMany(b => b.Week) // Assuming Months has a collection of Weeks
                .HasForeignKey(t => t.BookId);
            // One-to-Many Relationship Between user and Weeks
            modelBuilder.Entity<Week>()
                .HasOne(t => t.user)
                .WithMany(b => b.Week) // Assuming Months has a collection of Weeks
                .HasForeignKey(t => t.user_id);

            // One-to-Many Relationship Between Week and Journal
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.week)
                .WithMany(m => m.journal)  // Assuming Weeks has a collection of Journals
                .HasForeignKey(j => j.weekId);

            // One-to-Many Relationship Between Babybook and Journal
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.babyBook)
                .WithMany(b => b.Journals)  // Assuming BabyBook has a collection of Journals
                .HasForeignKey(j => j.BookId);

            // One-to-Many Relationship Between user and Journal
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.user)
                .WithMany(b => b.Journal)  // Assuming user has a collection of Journals
                .HasForeignKey(j => j.user_id);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This method will be used to configure the context options.
            optionsBuilder.EnableSensitiveDataLogging(); // Add this line to enable sensitive data logging
        }

    }
}
