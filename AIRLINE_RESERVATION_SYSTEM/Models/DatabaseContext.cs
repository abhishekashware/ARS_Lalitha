using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIRLINE_RESERVATION_SYSTEM.Models
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Admin> Admins { get; set; }
        
        public DbSet<Airport> Airports { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Seat> Seats { get; set; }

    }
}
