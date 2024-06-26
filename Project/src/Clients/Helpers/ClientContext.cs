using Microsoft.EntityFrameworkCore;
using Project.Clients.Models;

namespace Project.Clients.Helpers;

public class ClientContext(DbContextOptions<ClientContext> options) : DbContext(options)
{
    public virtual required DbSet<Individual> Individuals { get; set; }
    public virtual required DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Individual>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Individual");
            
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(25);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.Pesel)
                .IsRequired()
                .HasMaxLength(11);
        });
        
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Company");
            
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(25);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10); 

            entity.Property(e => e.Krs)
                .IsRequired()
                .HasMaxLength(14);
        });
    }

}
