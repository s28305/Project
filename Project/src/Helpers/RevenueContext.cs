using Microsoft.EntityFrameworkCore;
using Project.Authentication.Models;
using Project.Clients.Models;
using Project.SoftwareSystems.Models;

namespace Project.Helpers
{
    public class RevenueContext(DbContextOptions<RevenueContext> options) : DbContext(options)
    {
        public virtual required DbSet<Individual> Individuals { get; set; }
        public virtual required DbSet<Company> Companies { get; set; }
        public virtual required DbSet<SoftwareSystem> SoftwareSystems { get; set; }
        public virtual required DbSet<Contract> Contracts { get; set; }
        public virtual required DbSet<Discount> Discounts { get; set; }
        public virtual required DbSet<Payment> Payments { get; set; }
        
        public virtual required DbSet<Role> Roles { get; set; }

        public virtual required DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .ToTable("Clients"); 

            modelBuilder.Entity<Individual>(entity =>
            {
                entity.ToTable("Individuals");
                entity.HasBaseType<Client>();

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
                    .HasMaxLength(14);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies");
                entity.HasBaseType<Client>();

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
                entity.ToTable("SoftwareSystems");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("Contracts");

                entity.Property(c => c.StartDate)
                    .IsRequired();

                entity.Property(c => c.EndDate)
                    .IsRequired();

                entity.Property(c => c.IsSigned)
                    .IsRequired()
                    .HasDefaultValue(false);

                entity.Property(c => c.IsCancelled)
                    .IsRequired()
                    .HasDefaultValue(false);

                entity.HasOne(c => c.SoftwareSystem)
                    .WithMany()
                    .HasForeignKey(c => new { c.SoftwareSystemId, c.SoftwareVersion })
                    .HasPrincipalKey(s => new { s.Id, s.Version })
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Client)
                    .WithMany()
                    .HasForeignKey(c => c.ClientId);

                entity.Property(e => e.ConcurrencyToken)
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discounts");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Offer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(s => s.SoftwareSystem)
                    .WithMany()
                    .HasForeignKey(s => s.SoftwareSystemId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");

                entity.HasOne(p => p.Contract)
                    .WithMany(c => c.Payments)
                    .HasForeignKey(p => p.ContractId);
                
                entity.Property(e => e.ConcurrencyToken)
                    .IsConcurrencyToken();
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();

                entity.Property(u => u.Username).HasMaxLength(30);
                entity.Property(u => u.Password).HasMaxLength(256);

                entity.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.Id);
                entity.HasIndex(r => r.Name).IsUnique();
                entity.Property(r => r.Name).HasMaxLength(50);
            });
        }
    }
}
