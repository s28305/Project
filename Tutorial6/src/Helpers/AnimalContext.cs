using Microsoft.EntityFrameworkCore;
using Tutorial6.Models;

namespace Tutorial6.Helpers;

public class AnimalContext : DbContext
{

    public AnimalContext()
    {

    }

    public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
    {

    }

    public virtual required DbSet<Animal> Animals { get; set; }
    public virtual required DbSet<AnimalTypes> AnimalTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Animal");

            entity.Property(e => e.Name)
                .HasMaxLength(100);
            
            entity.Property(e => e.Description)
                .HasMaxLength(2000);
            
            entity.HasOne(a => a.AnimalType)
                .WithMany()
                .HasForeignKey(a => a.AnimalTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<AnimalTypes>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("AnimalTypes");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        modelBuilder.Entity<Animal>()
            .Property(a => a.ConcurrencyToken)
            .IsConcurrencyToken();
    }
    
}
