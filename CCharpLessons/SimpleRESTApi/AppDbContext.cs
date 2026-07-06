using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Device> Devices => Set<Device>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<User>(b =>
        {
            b.HasKey(u => u.Id);
            b.Property(u => u.Username).IsRequired();
            b.Property(u => u.CreatedUtc).IsRequired();
        });

        model.Entity<Device>(b =>
        {
            b.HasKey(d => d.Id);
            b.Property(d => d.Name).IsRequired();
            b.HasOne(d => d.User).WithMany(u => u.Devices).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Cascade);
            b.Property(d => d.CreatedUtc).IsRequired();
        });
    }
}
