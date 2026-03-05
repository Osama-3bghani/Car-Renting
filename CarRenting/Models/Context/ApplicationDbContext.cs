using Microsoft.EntityFrameworkCore;

namespace CarRenting.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        // FIX 4: Removed hardcoded connection string from OnConfiguring().
        // DbContext now receives options via constructor injection from Program.cs.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CarGallery> CarGallery { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CarImages> CarImages { get; set; }
        public DbSet<CarGalleryImages> CarGalleryImages { get; set; }
        public DbSet<CarImagesCarOwner> CarImagesCarOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert Enums to strings for human-readable DB values
            modelBuilder.Entity<Car>().Property(e => e.ProvidedBy).HasConversion<string>();
            modelBuilder.Entity<Car>().Property(e => e.Availability).HasConversion<string>();
            modelBuilder.Entity<Car>().Property(e => e.CarType).HasConversion<string>();
            modelBuilder.Entity<Car>().Property(e => e.CarStatus).HasConversion<string>();
            modelBuilder.Entity<Booking>().Property(e => e.RequestStatus).HasConversion<string>();

            // Composite PK for CarImagesCarOwner junction table
            modelBuilder.Entity<CarImagesCarOwner>().HasKey(e => new { e.CarId, e.CarImagesId, e.CarOwnerId });
            modelBuilder.Entity<CarImagesCarOwner>()
                .HasOne(e => e.Car).WithMany(e => e.CarImagesCarOwners)
                .HasForeignKey(e => e.CarId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<CarImagesCarOwner>()
                .HasOne(e => e.CarOwner).WithMany(e => e.CarImagesCarOwners)
                .HasForeignKey(e => e.CarOwnerId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<CarImagesCarOwner>()
                .HasOne(e => e.CarImages).WithMany(e => e.CarImagesCarOwners)
                .HasForeignKey(e => e.CarImagesId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
