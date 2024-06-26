using Microsoft.EntityFrameworkCore;
using Project.Clients.Models;
using Project.SoftwareSystems.Models;

namespace Project.Clients.Helpers;

public class ClientContext(DbContextOptions<ClientContext> options) : DbContext(options)
{
    public virtual required DbSet<Individual> Individuals { get; set; }
    public virtual required DbSet<Company> Companies { get; set; }
    public virtual required DbSet<SoftwareSystem> SoftwareSystems { get; set; }
    
    public virtual required DbSet<Contract> Contracts { get; set; }
    
    public virtual required DbSet<Discount> DbSet { get; set; }
    
    public virtual required DbSet<Payment> Payments { get; set; }

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

        modelBuilder.Entity<SoftwareSystem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("SoftwareSystem");
            
            entity.Property(a => a.ConcurrencyToken)
                .IsConcurrencyToken();
        });
        
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Contract");
        });
        
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Discount");
        });
        
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Payment");
        });
        
    }

}
