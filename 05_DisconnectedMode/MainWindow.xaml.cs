using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_DisconnectedMode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection conn = null;
        private SqlDataAdapter da = null;
        private DataSet set = null;
        public MainWindow()
        {
            InitializeComponent();
            //    .ConnectionString;
            var connection = ConfigurationManager.ConnectionStrings["SportShopDbConnection"]
                .ConnectionString;
            conn = new SqlConnection(connection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try { 

                string sql = commandTextBox.Text;
                da = new SqlDataAdapter(sql, conn);
                new SqlCommandBuilder(da);

                set = new DataSet();

                da.Fill(set, "MyTable");

                dataGrid.ItemsSource = set.Tables["MyTable"].DefaultView;
            } 
            catch(Exception ex) { MessageBox.Show(ex.Message); }
     
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                da.Update(set, "MyTable");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}