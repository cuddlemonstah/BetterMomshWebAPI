using Microsoft.EntityFrameworkCore;

namespace BetterMomshWebAPI.EFCore
{
    public class API_DataContext : DbContext
    {
        public API_DataContext(DbContextOptions<API_DataContext> options)
            : base(options)
        {
        }
        public DbSet<userCred>? UserCred { get; set; }
        public DbSet<RefreshTokens>? RefreshTokens { get; set; }
        public DbSet<userInfo> UserInfo { get; set; }
        public DbSet<BabyBook> BabyBook { get; set; }
        public DbSet<Trimester> trimesters { get; set; }
        public DbSet<Months> months { get; set; }
        public DbSet<Journal> journal { get; set; }
        public DbSet<Weeks> week { get; set; }
        public DbSet<TokenBlacklist> blacklist { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<TokenBlacklist>()
                .HasKey(x => x.Id);
            // One-to-One Relationship Between UserCred and UserInfo
            modelBuilder.Entity<userCred>()
                .HasOne(uc => uc.UserInfo)
                .WithOne(ui => ui.userCred) // Assuming UserInfo has a reference to UserCred
                .HasForeignKey<userInfo>(ui => ui.user_id);

            modelBuilder.Entity<userCred>()
                .HasOne(uc => uc.RefreshTokens)
                .WithOne(ui => ui.userCred) // Assuming RefreshToken has a reference to UserCred
                .HasForeignKey<RefreshTokens>(ui => ui.user_id);

            // One-to-Many Relationship Between UserCred and BabyBook
            modelBuilder.Entity<BabyBook>()
                .HasOne(b => b.UserCred)
                .WithMany(uc => uc.BabyBooks) // Assuming UserCred has a collection of BabyBooks
                .HasForeignKey(b => b.user_id);

            // One-to-Many Relationship Between BabyBook and Trimester
            modelBuilder.Entity<Trimester>()
                .HasOne(t => t.babyBook)
                .WithMany(b => b.trim) // Assuming BabyBooks has a collection of Trimesters
                .HasForeignKey(t => t.BookId);
            modelBuilder.Entity<Months>()
                .HasOne(t => t.trim)
                .WithMany(b => b.mon) // Assuming Trimesters has a collection of Months
                .HasForeignKey(t => t.TrimesterId);
            modelBuilder.Entity<Weeks>()
                .HasOne(t => t.mon)
                .WithMany(b => b.weeks) // Assuming Months has a collection of Weeks
                .HasForeignKey(t => t.MonthId);
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.week)
                .WithMany(m => m.journal)  // Assuming Weeks has a collection of Journals
                .HasForeignKey(j => j.weekId);

            modelBuilder.Entity<Journal>()
                .HasOne(j => j.babyBook)
                .WithMany(b => b.journal)  // Assuming BabyBook has a collection of Journals
                .HasForeignKey(j => j.BookId);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This method will be used to configure the context options.
            optionsBuilder.EnableSensitiveDataLogging(); // Add this line to enable sensitive data logging
        }

    }
}
