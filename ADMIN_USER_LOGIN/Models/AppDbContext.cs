using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ADMIN_USER_LOGIN.Models
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }


        public DbSet<Airport> Airports { get; set; }

        public DbSet<SearchData> FilteredFlights  { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Seat> GetSeatsByFId { get; set; }
    }
}
