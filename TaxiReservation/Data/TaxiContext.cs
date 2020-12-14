using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace TaxiReservation.Data
{
    public sealed class TaxiContext : DbContext
    {
        public DbSet<Taxi> Cars { get; set; }

        public TaxiContext(DbContextOptions<TaxiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Taxi>().HasData(new List<Taxi>
            {
                new Taxi
                {
                    Id = 1,
                    Car = "Honda Civic",
                    Colour = "Green",
                    CarNumber = "А228УЕ777RUS",
                    IsReserved = false,
                    DriverPhoneNumber = 89256787664
                }
            });
        }
    }
}
