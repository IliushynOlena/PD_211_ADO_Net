using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_IntroToEF
{
    public class AirplaneDbContext : DbContext
    {
        public AirplaneDbContext()
        {
            this.Database.EnsureCreated();
            
        }
        //Collections
        public DbSet<Client> Clients { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-3HG9UVT\SQLEXPRESS;
                                        Initial Catalog = NewAirplanePD211;
                                        Integrated Security=True;
                                        Connect Timeout=2;Encrypt=False;
                                         Trust Server Certificate=False;
                                        Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

    }
    //Entities
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        //Relational type : Many to Many (*....*)
        public ICollection<Flight> Flights { get; set; }
    }
    public class Flight
    {
        public int Id { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        //Relational type : One to Many (1....*)
        public int AirplaneId { get; set; }//foreign key
        public Airplane Airplane { get; set; }//null
        //Relational type : Many to Many (*....*)
        public ICollection<Client> Clients { get; set; }

    }
    public class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int MaxPassanger { get; set; }
        //Relational type : Many to Many (*....*)
        public ICollection<Flight> Flights { get; set; }
    }
}
