using _07_IntroToEF.Entities;
using System;
using System.Linq;

namespace _07_IntroToEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////value types = not null in database
            int? a = null;
            //int b = 0;//not null
            //DateTime day = null;
            //DateTime date = new DateTime(2000, 3, 6);//not null
            ////references type = null in database
            //string name = null;
            //AirplaneDbContext c = null;
            AirplaneDbContext context = new AirplaneDbContext();    
            context.Clients.Add(new Client()
            {
                 Name = "Volodia",
                 Birthday = new DateTime(2006,12,4),  
                  Email = "volodia@gmail.com"
            });
            //context.SaveChanges();

            foreach (var item in context.Clients)
            {
                Console.WriteLine($"Client : name - {item.Name}. Email:  {item.Email}. Birthday :" +
                    $"{item.Birthday?.ToShortDateString()} ");
            }
            //Linq to Entities

            var filteredFlights =   context.Flights
                                    .Where(f => f.ArrivalCity == "Lviv")
                                    .OrderBy(f => f.DepartureTime);

            foreach (var f in filteredFlights)
            {
                Console.WriteLine($"Flight : {f.Number}. From : {f.DepartureCity,15} to {f.ArrivalCity}" +
                    $"Date :{f.DepartureTime.ToShortDateString(),20}");

            }

            var client = context.Clients.Find(1);

            if(client != null)
            {
                context.Clients.Remove(client); 
                context.SaveChanges();  
            }
        }
    }
}
