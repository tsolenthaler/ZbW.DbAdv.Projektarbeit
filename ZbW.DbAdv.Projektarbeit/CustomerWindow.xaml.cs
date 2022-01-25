using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly DataGridChef _dataGridChef;
        private readonly List<int> _comboBoxColumnIndices = new List<int>() { 1, 2 };
        private readonly List<int> _datePickerColumnIndices = new List<int>() { 1, 2 };

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        private readonly MainWindow mainWindow;
        

        public CustomerWindow(MainWindow mainWindow)
        {
            DataContext = this;
            InitializeComponent();
            this.mainWindow = mainWindow;

            _dataGridChef = new DataGridChef(CustomerDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateSampleData();
        }

        private void GenerateSampleData()
        {
            Customer customer = new Customer();
            customer.Id = 1;
            customer.FirstName = "Hans";
            customer.LastName = "Muster";
            customer.Address = new Address();
            customer.Address.Street = "Musterstrasse";

            Customers.Add(customer);

            Customer customer2 = new Customer();
            customer2.Id = 2;
            customer2.FirstName = "Franz";
            customer2.LastName = "Gluggeri";
            customer2.Address = new Address();
            customer2.Address.Street = "Teststrasse";

            Customers.Add(customer2);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Show();
        }
    }
}
