using System;
using System.ComponentModel;
using System.Windows;
using Autofac;
using BusinessLayer;
using DataAccessLayer.Customer;
using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Customer;
using DataAccessLayer.Invoice;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly Autofac.IContainer container;

        public BusinessManager BusinessManager { get; }

        public MainWindow() {
            InitializeComponent();
            container = new IoCSetup().SetupContainer();
            BusinessManager = container.Resolve<BusinessManager>();
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
                BusinessManager.MigrateDatabase();
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
