using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_LinqToDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            foreach (var item in context.Products)
            {
                Console.WriteLine( $" {item.Name,30}, {item.CostPrice,7}, {item.Producer,20}");
            }
            Console.WriteLine();
            foreach (var item in context.Salles)
            {
                Console.WriteLine($" {item.ClientId,30}, {item.EmployeeId,7}, {item.Price,20}");
            }
        }
    }
}
