using Microsoft.EntityFrameworkCore;
using Tutorial6.Models;

namespace Tutorial6.Helpers
{
    public class AnimalClinicContext : DbContext
    {
        public AnimalClinicContext()
        {
        }

        public AnimalClinicContext(DbContextOptions<AnimalClinicContext> options) : base(options)
        {
        }
        
        public virtual required DbSet<Animal> Animals { get; set; }
        public virtual required DbSet<AnimalTypes> AnimalTypes { get; set; }
        public virtual required DbSet<Employee> Employees { get; set; }
        public virtual required DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Animal");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(2000);

                entity.HasOne(a => a.AnimalType)
                    .WithMany()
                    .HasForeignKey(a => a.AnimalTypesId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.ConcurrencyToken)
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<AnimalTypes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AnimalTypes");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Employee");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.HasIndex(e => new { e.PhoneNumber, e.Email })
                    .IsUnique();
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Visit");

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.HasOne(v => v.Employee)
                    .WithMany()
                    .HasForeignKey(v => v.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(v => v.Animal)
                    .WithMany()
                    .HasForeignKey(v => v.AnimalId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(v => v.ConcurrencyToken)
                    .IsConcurrencyToken();
            });
        }
    }
}
