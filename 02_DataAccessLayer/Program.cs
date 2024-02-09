using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace _02_DataAccessLayer
{
    class SportShopDB
    {
        private SqlConnection connection;
        private string connectionString;
        public SportShopDB(string serverName, string dbName)
        {
            connectionString = $@"Data Source = {serverName};
                                        Initial Catalog = {dbName};
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
            //string cmdText = $@"INSERT INTO Products
            //                VALUES ('{product.Name}', 
            //                        '{product.Type}', 
            //                         {product.Quatity}, 
            //                         {product.CostPrice}, 
            //                        '{product.Producer}', 
            //                         {product.Price})";
            string cmdText = $@"INSERT INTO Products
                            VALUES (@name, 
                                    @type, 
                                    @quantity, 
                                    @costPrice, 
                                    @producer, 
                                    @price)";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("type", product.Type);
            cmd.Parameters.AddWithValue("quantity", product.Quatity);
            cmd.Parameters.AddWithValue("costPrice", product.CostPrice);
            cmd.Parameters.AddWithValue("producer", product.Producer);
            cmd.Parameters.AddWithValue("price", product.Price);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Product was added to database!");
        }
        private List<Product> GetProductsByQuery(SqlDataReader reader)
        {          
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
        public List<Product> GetAll()//Read
        {
            string cmdText = @"select * from Products";
            SqlCommand command = new SqlCommand(cmdText, connection);
            SqlDataReader reader = command.ExecuteReader();
            return this.GetProductsByQuery(reader);
           
        }
        public Product GetOneProduct(int id)
        {            
            string cmdText = $@"select * from Products where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, connection);
            SqlDataReader reader = command.ExecuteReader();
            return this.GetProductsByQuery(reader).FirstOrDefault()!;
        }
        public List<Product> GetAllByName(string name)
        {
            //name = "Ball';drop database SportShop;--";
            //Ball';drop database SportShop;--
            string cmdText = $@"select * from Products where Name = '{name}'";

            SqlCommand command = new SqlCommand(cmdText, connection);
            //command.Parameters.Add("name", System.Data.SqlDbType.NVarChar).Value = name;
            SqlParameter parameter = new SqlParameter()
            {
                ParameterName = "name",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Value = name
            };
            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();
            return this.GetProductsByQuery(reader);
        }
        public void Update(Product product)
        {
            string cmdText = $@"UPDATE Products
                              SET Name =  @name, 
                                TypeProduct = @type, 
                                Quantity = @quantity, 
                                CostPrice = @costPrice, 
                                Producer = @producer, 
                                Price =@price
                                where Id = {product.Id}";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("type", product.Type);
            cmd.Parameters.AddWithValue("quantity", product.Quatity);
            cmd.Parameters.AddWithValue("costPrice", product.CostPrice);
            cmd.Parameters.AddWithValue("producer", product.Producer);
            cmd.Parameters.AddWithValue("price", product.Price);
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
