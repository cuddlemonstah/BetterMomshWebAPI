using Microsoft.EntityFrameworkCore;

namespace BetterMomshWebAPI.EFCore
{
    public class API_DataContext : DbContext
    {
        public API_DataContext(DbContextOptions<API_DataContext> options)
            : base(options)
        {
        }
        public DbSet<userCred> UserCred { get; set; }
        public DbSet<userInfo> UserInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<userCred>()
            .HasOne(uc => uc.UserInfo)
            .WithOne(ui => ui.userCred)
            .HasForeignKey<userInfo>(ui => ui.user_id);
        }
    }
}
