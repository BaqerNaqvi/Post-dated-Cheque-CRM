using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbContexts;

public partial class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=pdc;uid=root;pwd=P@ssw0rd", ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);

        SeedRoles(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1dcf6b3f-500c-4cd4-8ad9-9e5eb6d6df05", Name = "SuperAdmin", ConcurrencyStamp = "1", NormalizedName = "SUPERADMIN" },
                new IdentityRole() { Id = "213f2477-e428-4676-b74d-cc812ced1361", Name = "Admin", ConcurrencyStamp = "2", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "f87fce74-44df-4563-9b60-9edd395cffdb", Name = "Manager", ConcurrencyStamp = "3", NormalizedName = "MANAGER" }
             );
    }
}
