using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace webapicore6.Models.Identity
{
    public class IdentityDbContext:IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole() { Name = "SuperAdmin", ConcurrencyStamp = "1", NormalizedName = "SuperAdmin" },
                    new IdentityRole() { Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "Manager" },
                    new IdentityRole() { Name = "Analyst", ConcurrencyStamp = "3", NormalizedName = "Analyst" },
                    new IdentityRole() { Name = "QA", ConcurrencyStamp = "4", NormalizedName = "QA" }
                 );
        }
    }
}
