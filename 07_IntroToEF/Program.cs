using System;

namespace _07_IntroToEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirplaneDbContext context = new AirplaneDbContext();    
            context.Clients.Add(new Client()
            {
                 Name = "Max",
                 Birthday = new DateTime(2006,12,4),  
                  Email = "max@gmail.com"
            });
            context.SaveChanges();

            foreach (var item in context.Clients)
            {
                Console.WriteLine($"Client : name - {item.Name}. Email:  {item.Email}. Birthday :" +
                    $"{item.Birthday.ToShortDateString()} ");
            }
        }
    }
}
