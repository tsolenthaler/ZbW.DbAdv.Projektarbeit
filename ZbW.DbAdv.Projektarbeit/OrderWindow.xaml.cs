using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window, INotifyPropertyChanged
    {
        private DataGridChef dataGridChef;
        private readonly List<int> comboBoxColumnIndices = new List<int>() {1, 2};
        private readonly List<int> datePickerColumnIndices = new List<int>() { 1, 2 };
        private MainWindow mainWindow;
        //public DataTable OrderDataTable;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public OrderWindow(MainWindow mainWindow)
        {
            DataContext = this;
            InitializeComponent();
            this.mainWindow = mainWindow;

            //this.OrderDataTable = DefineOrderDataTable();
            //orderDataGrid.DataContext = OrderDataTable;
            //dataGridChef = new DataGridChef(orderDataGrid, OrderDataTable, comboBoxColumnIndices, datePickerColumnIndices);
            //FillTableWithTestData();
        }

        private void DefineOrders()
        {
            Orders.Add(new Order { Id = 1 });
            Orders.Add(new Order { Id = 2 });
            Orders.Add(new Order { Id = 3 });     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DefineOrders();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }

        private DataTable DefineOrderDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("Customer", typeof(string)));
            return dataTable;
        }

        //private void FillTableWithTestData()
        //{
        //    var orderRow = OrderDataTable.NewRow();
        //    orderRow[0] = 1;
        //    orderRow[1] = DateTime.Now;
        //    orderRow[2] = "Hans Muster";
        //    OrderDataTable.Rows.Add(orderRow);
        //}



        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
