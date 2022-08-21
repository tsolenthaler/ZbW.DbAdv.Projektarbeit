using System;
using System.Windows;
using BusinessLayer;
using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Context;
using DataAccessLayer.Customer;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;

namespace PresentationLayer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public BusinessManager BusinessManager { get; } = new BusinessManager(new CustomerRepository(new SetupDB()), 
            new ArticleRepository(new SetupDB()), new ArticleGroupRepository(new SetupDB()),
            new OrderRepository(new SetupDB()), new OrderPositionRepository(new SetupDB()));

        public MainWindow() {
            InitializeComponent();
        }

        private void Cmd_OrdersWindow_Click(object sender, RoutedEventArgs e) {
            OrderWindow orderWindow = new OrderWindow(this);
            orderWindow.Show();
            this.Hide();
        }

        private void Cmd_CustomerWindow_Click(object sender, RoutedEventArgs e) {
            CustomerWindow customerWindow = new CustomerWindow(this);
            customerWindow.Show();
            this.Hide();
        }

        private void Cmd_InvoiceWindow_Click(object sender, RoutedEventArgs e) {
            InvoiceWindow invoiceWindow = new InvoiceWindow(this);
            invoiceWindow.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            try {
                BusinessManager.DataAccessManager.MigrateDatabase();
            }
            catch (Exception ex) {
                MessageBox.Show("Not able to migrate database - maybe you haven't configured the connection string yet? \r\n \r\nConfigure it in file DataAccessLayer/Context/SetupDB.cs \r\n \r\nError Message: \r\n" + ex.Message + " Inner Exception: " + ex.InnerException?.Message);
            }

        }

        private void Cmd_ArticleWindow_Click(object sender, RoutedEventArgs e) {
            ArticleWindow articleWindow = new ArticleWindow(this);
            articleWindow.Show();
            this.Hide();
        }

        private void Cmd_YearEndingWindow_Click(object sender, RoutedEventArgs e) {
            YearEndingWindow yearEndingWindow = new YearEndingWindow(this);
            yearEndingWindow.Show();
            this.Hide();
        }
    }
}
