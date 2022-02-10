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

        private void SetGUIToModifyMode() {
            Cmd_AddOrder.IsEnabled = false;
            Cmd_ModifyOrder.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveOrder.IsEnabled = true;
            Cmd_Cancel.IsEnabled = true;
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "view mode"
        ///</summary>
        private void SetGUIToViewMode() {
            Cmd_AddOrder.IsEnabled = true;
            Cmd_ModifyOrder.IsEnabled = true;
            Cmd_Delete.IsEnabled = true;
            Cmd_SaveOrder.IsEnabled = false;
            Cmd_Cancel.IsEnabled = false;
        }

        ///<summary>
        ///Deactivates buttons and textfields for GUI "full view mode", applicable when searching content
        ///</summary>
        private void SetGUIToFullViewMode() {
            Cmd_AddOrder.IsEnabled = false;
            Cmd_ModifyOrder.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveOrder.IsEnabled = false;
            Cmd_Cancel.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExampleOrders();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }

        private void Cmd_AddOrder_Click(object sender, RoutedEventArgs e) {


            SetGUIToModifyMode();
        }

        private void Cmd_SaveOrder_Click(object sender, RoutedEventArgs e) {


            SetGUIToViewMode();
        }

        private void Cmd_ModifyOrder_Click(object sender, RoutedEventArgs e) {


            SetGUIToModifyMode();
        }

        private void Cmd_Delete_Click(object sender, RoutedEventArgs e) {

        }

        private void Cmd_Cancel_Click(object sender, RoutedEventArgs e) {


            SetGUIToViewMode();
        }

        private void Cmd_SearchOrders_Click(object sender, RoutedEventArgs e) {
            
            // if
            SetGUIToFullViewMode();

            // else
            SetGUIToViewMode();
        }

        private void Cmd_SaveOrder_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
