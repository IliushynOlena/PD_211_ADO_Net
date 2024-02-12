using _04_data_access;
using System.Configuration;
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

namespace _03_wpf_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SportShopDB dB;
        public MainWindow()
        {
            InitializeComponent();
            //var connection = ConfigurationManager.ConnectionStrings["SportShopDbConnection"]
            //    .ConnectionString;
            var connection = ConfigurationManager.ConnectionStrings["testDbConnection"]
                .ConnectionString;
            dB = new SportShopDB(connection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           dataGrid.ItemsSource =   dB.GetAll();
        }
    }
}