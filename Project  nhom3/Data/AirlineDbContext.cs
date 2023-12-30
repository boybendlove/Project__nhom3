using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project__nhom3.Models;

namespace Project__nhom3.Data
    {
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
        {
        }

        public DbSet<Airplane> Airplane { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Models.Customer> Customer { get; set; }
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Reschedule_Ticket> Reschedule_Ticket { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Ticket_type> Ticket_type { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Ticket)
                .WithMany()
                .HasForeignKey(b => b.ticket_id);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.customer_id);

            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.customer_id);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Airplane)
                .WithMany()
                .HasForeignKey(f => f.airplane_id);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Location)
                .WithMany()
                .HasForeignKey(f => f.location_id);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.booking_id);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.customer_id);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Ticket)
                .WithMany()
                .HasForeignKey(p => p.ticket_id);

            modelBuilder.Entity<Airplane>()
                .HasOne(a => a.Seat)
                .WithMany()
                .HasForeignKey(a => a.seat_id);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany()
                .HasForeignKey(t => t.flight_id);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.customer_id);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Passenger)
                .WithMany()
                .HasForeignKey(t => t.passenger_id);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.seat_id);

            modelBuilder.Entity<Reschedule_Ticket>(entity =>
            {
                entity.ToTable("Reschedule_Ticket");
                entity.HasKey(e => e.reschedule_ticket_id);
            });

            modelBuilder.Entity<Ticket_type>(entity =>
            {
                entity.ToTable("Ticket_type");
                entity.HasKey(e => e.ticket_type_id);
            });
            modelBuilder.Entity<Flight>()
        .Property(f => f.price_economy)
        .HasColumnType("decimal(10,2)"); // Sử dụng kiểu dữ liệu decimal(10,2) cho cột Price

            modelBuilder.Entity<Flight>()
        .Property(f => f.price_business)
        .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.price)
                .HasColumnType("decimal(10,2)"); // Sử dụng kiểu dữ liệu decimal(10,2) cho cột Price

        }
    }
}
    



