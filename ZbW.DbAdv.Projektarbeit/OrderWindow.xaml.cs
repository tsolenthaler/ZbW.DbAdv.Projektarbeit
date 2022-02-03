using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private DataGridChef dataGridChef;
        private readonly List<int> comboBoxColumnIndices = new List<int>() {1, 2};
        private readonly List<int> datePickerColumnIndices = new List<int>() { 1, 2 };

        private MainWindow mainWindow;
        

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<OrderPosition> OrderPositions { get; set; } = new ObservableCollection<OrderPosition>();

        public OrderWindow(MainWindow mainWindow)
        {
            DataContext = this;
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void ExampleOrders()
        {
            Orders.Add(new Order { Id = 1 });
            Orders.Add(new Order { Id = 2 });
            Orders.Add(new Order { Id = 3 });     

            OrderPositions.Add(new OrderPosition{ Id = 10 });
            OrderPositions.Add(new OrderPosition{ Id = 20 });
            OrderPositions.Add(new OrderPosition{ Id = 30 });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExampleOrders();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }

    }
}
