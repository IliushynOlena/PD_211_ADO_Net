using System.Data.SqlClient;
using System.Text;

namespace _02_DataAccessLayer
{
    class SportShopDB
    {
        private SqlConnection connection;
        private string connectionString;
        public SportShopDB()
        {
            connectionString = @"Data Source = DESKTOP-3HG9UVT\SQLEXPRESS;
                                        Initial Catalog = SportShop;
                                        Integrated Security = true;Connect Timeout = 2";
            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Database is ready to work!");
        }

        //CRUD
        //[C]reate
        //[R]ead 
        //[U]pdate
        //[D]elete
        public void Create(Product product)
        {
            string cmdText = $@"INSERT INTO Products
                            VALUES ('{product.Name}', 
                                    '{product.Type}', 
                                     {product.Quatity}, 
                                     {product.CostPrice}, 
                                    '{product.Producer}', 
                                     {product.Price})";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product was added to database!");
        }
        public List<Product> GetAll()//Read
        {
            string cmdText = @"select * from Products";
            SqlCommand command = new SqlCommand(cmdText, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                products.Add(new Product()
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1],
                    Type = (string)reader[2],
                    Quatity = (int)reader[3],
                    CostPrice = (int)reader[4],
                    Producer = (string)reader[5],
                    Price = (int)reader[6]
                });
            }        
            reader.Close();
            return products;
        }
        public Product GetOneProduct(int id)
        {
            string cmdText = $@"select * from Products where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, connection);
            SqlDataReader reader = command.ExecuteReader();
            Product product = new Product();
            while (reader.Read())
            {
                product.Id = (int)reader[0];
                product.Name = (string)reader[1];
                product.Type = (string)reader[2];
                product.Quatity = (int)reader[3];
                product.CostPrice = (int)reader[4];
                product.Producer = (string)reader[5];
                product.Price = (int)reader[6];
               
            }
            reader.Close();
            return product;
        }
        public void Update(Product product)
        {
            string cmdText = $@"UPDATE Products
                              SET Name =  '{product.Name}', 
                                TypeProduct = '{product.Type}', 
                                Quantity = {product.Quatity}, 
                                CostPrice = {product.CostPrice}, 
                                Producer = '{product.Producer}', 
                                Price ={product.Price}
                                where Id = {product.Id}";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product was updated to database!");

        }
        public void Delete(int id)
        {
            string cmdText = $@"delete Products where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText,connection);
            command.ExecuteNonQuery();  
        }
        public void CreateSalle(Salles salles)
        {
            string cmdText = $@"insert into Salles
                         values({salles.ProductId}, 
                                {salles.Price},                          
                                {salles.Quantity}, 
                                {salles.EmployeeId}, 
                                {salles.ClientId},
                                {"'" + salles.SaleDate.ToString("yyyy-MM-dd") + "'"})";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            Console.WriteLine(salles.SaleDate.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Salles has added!");
        }
    }
    public class Salles
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SportShopDB dB = new SportShopDB();
            Salles salles = new Salles()
            {
                ProductId = 2,
                Price = 1250,
                Quantity = 1,
                EmployeeId = 3,
                ClientId = 1,
                SaleDate = DateTime.Now  //new DateTime(2024,2,15)
            };
            dB.CreateSalle(salles);

            //Console.OutputEncoding = Encoding.UTF8;
            Product product = new Product()
            {
                Name = "Bottle XL",
                Type = "Equipment",
                Quatity = 25, 
                CostPrice = 450, 
                Producer = "Poland", 
                Price = 600
            };
            //dB.Create(product);
            //dB.Delete(39);
            //foreach (var item in dB.GetAll())
            //{
            //    Console.WriteLine($"{item.Id,5} {item.Name,-20}{item.Price,10}");
            //}  
            //Product pr =  dB.GetOneProduct(1);
            //Console.WriteLine($"{pr.Id,5} {pr.Name,-20}{pr.Price,10} {pr.CostPrice,10}");
            //pr.CostPrice -= 500;
            //pr.Price -= 500;
            //Console.WriteLine($"{pr.Id,5} {pr.Name,-20}{pr.Price,10} {pr.CostPrice,10}");

            ////dB.Update(pr);


            //dB.Delete(53);
            //foreach (var item in dB.GetAll())
            //{
            //    Console.WriteLine($"{item.Id,5} {item.Name,-20}{item.Price,10}");
            //}

        }
    }
}
