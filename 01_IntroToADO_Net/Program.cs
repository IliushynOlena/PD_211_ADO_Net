using System.Data.SqlClient;
using System.Text;

namespace _01_IntroToADO_Net
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //connection string: Property = value;property2:value2
            string connectionString = @"Data Source = DESKTOP-3HG9UVT\SQLEXPRESS;
                                        Initial Catalog = SportShop;
                                        Integrated Security = true;Connect Timeout = 2";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            Console.WriteLine("Connected success!!!!");

            #region Execute Non-Query
            //string cmdText = @"INSERT INTO Products
            //                  VALUES ('Soccer', 'Equipment', 25, 2000, 'Ukraine', 2500)";

            //SqlCommand command = new SqlCommand(cmdText, sqlConnection);
            //command.CommandTimeout = 5; // default - 30sec
            ////// ExecuteNonQuery - виконує команду яка не повертає результат 
            /////(insert, update, delete...),
            //////але метод повертає кількітсь рядків, які були задіяні
            /////
            //int rows = command.ExecuteNonQuery();
            //Console.WriteLine(rows + " rows affected!");
            #endregion
            #region Execute Scalar
            //string cmdText = @"select AVG(Price) from Products";

            //SqlCommand command = new SqlCommand(cmdText, sqlConnection);

            //// Execute Scalar - виконує команду, яка повертає одне значення
            //int res = (int)command.ExecuteScalar();

            //Console.WriteLine("Result: " + res);
            #endregion
            #region Execute Reader
            string cmdText = @"select * from Products";

            SqlCommand command = new SqlCommand(cmdText, sqlConnection);

            // ExecuteReader - виконує команду select та повертає результат у вигляді
            // DbDataReader
            SqlDataReader reader = command.ExecuteReader();           

            Console.OutputEncoding = Encoding.UTF8;



            // відображається назви всіх колонок таблиці
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($" {reader.GetName(i),17}");
            }
            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------------------");

            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($" {reader[i],17} ");
                }
                Console.WriteLine();
            }

           reader.Close();
            #endregion

            // disconnect
            sqlConnection.Close();
        }
    }
}
