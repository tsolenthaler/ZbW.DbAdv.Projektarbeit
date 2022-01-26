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

        //no ComboBox or DatePicker necessary for Customer Window
        private readonly List<int> _comboBoxColumnIndices = new List<int>() {};
        private readonly List<int> _datePickerColumnIndices = new List<int>() {};

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
            customer.ReadOnly = true;

            Customers.Add(customer);

            Customer customer2 = new Customer();
            customer2.Id = 2;
            customer2.FirstName = "Franz";
            customer2.LastName = "Gluggeri";
            customer2.Address = new Address();
            customer2.Address.Street = "Teststrasse";
            customer2.ReadOnly = false;

            Customers.Add(customer2);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Show();
        }

        private void Cmd_AddOrder_Click(object sender, RoutedEventArgs e)
        {
            Customers[0].ReadOnly = false;
        }

        private void CustomerDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _dataGridChef.BlockReadOnlyRows(e);
        }
    }
}
