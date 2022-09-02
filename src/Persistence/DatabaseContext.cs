using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Persintence;

public class DatabaseContext : DbContext{

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    public DbSet<Person> persons { get; set; }
    public DbSet<Contract> contracts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder){
        builder.Entity<Person>(table => {
            table.HasKey(e => e.id);
            table
                .HasMany(e => e.contracts)
                .WithOne()
                .HasForeignKey(c => c.personId);
        });

        builder.Entity<Contract>(table => {
            table.HasKey(e => e.id);
        });
        
    }
}