using _04_data_access.Model;
using _04_data_access;
using System.Text;
using System.Xml;

namespace _02_DataAccessLayer
{

    class Program
    {
        static void Main(string[] args)
        {
            SportShopDB dB = new SportShopDB("DESKTOP-3HG9UVT\\SQLEXPRESS", "SportShop");
            //Salles salles = new Salles()
            //{
            //    ProductId = 2,
            //    Price = 1250,
            //    Quantity = 1,
            //    EmployeeId = 3,
            //    ClientId = 1,
            //    SaleDate = DateTime.Now  //new DateTime(2024,2,15)
            //};
            //dB.CreateSalle(salles);

            Console.OutputEncoding = Encoding.UTF8;
            Product product = new Product()
            {
                Name = "Trousers",
                Type = "Clothes",
                Quatity = 10,
                CostPrice = 2000,
                Producer = "Poland",
                Price = 2500
            };
            dB.Create(product);
            //dB.Delete(39);
            Console.WriteLine("Enter product name to search : ");
            string name = Console.ReadLine();   


            List<Product> products = dB.GetAllByName(name);
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Id,5} {item.Name,-20}{item.CostPrice,10}");
            }
            Product pr =  dB.GetOneProduct(54);
            //Console.WriteLine($"{pr.Id,5} {pr.Name,-20}{pr.Price,10} {pr.CostPrice,10}");
            pr.CostPrice += 500;
            pr.Price += 500;
            Console.WriteLine($"{pr.Id,5} {pr.Name,-20}{pr.Price,10} {pr.CostPrice,10}");

            dB.Update(pr);

            //int id = 53;
            //dB.Delete(id);
            foreach (var item in dB.GetAll())
            {
                Console.WriteLine($"{item.Id,5} {item.Name,-20}{item.CostPrice,10}");
            }

        }
    }
}
