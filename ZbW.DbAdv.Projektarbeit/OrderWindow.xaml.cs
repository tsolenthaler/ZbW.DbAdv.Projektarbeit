using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer {
    /// <summary>
    /// Interaktionslogik für OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window {

        private MainWindow mainWindow;

        private readonly DataGridChef _orderDataGridChef;
        private readonly DataGridChef _orderPositionDataGridChef;

        //no ComboBox or DatePicker necessary for Customer Window
        private readonly List<int> _comboBoxColumnIndices = new List<int>() { };
        private readonly List<int> _datePickerColumnIndices = new List<int>() { };

        public ObservableCollection<Customer> Customers { get => mainWindow.BusinessManager.Customers; }

        public ObservableCollection<Order> Orders { get => mainWindow.BusinessManager.Orders; }
        public ObservableCollection<OrderPosition> OrderPositions { get => mainWindow.BusinessManager.OrderPositions; }

        public BusinessManager BusinessManager { get => mainWindow.BusinessManager; }

        public OrderWindow(MainWindow mainWindow) {
            DataContext = this;
            this.mainWindow = mainWindow;

            InitializeComponent();

            _orderDataGridChef = new DataGridChef(OrderDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            _orderPositionDataGridChef = new DataGridChef(OrderPositionDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            Resources["readOnlyColor"] = _orderDataGridChef.ReadOnlyFieldColor;

            try {
                BusinessManager.LoadAllOrdersFromDb();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
            }
        }

        private void SetGUIToModifyMode() {
            Cmd_AddOrder.IsEnabled = false;
            Cmd_ModifyOrder.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveOrder.IsEnabled = true;
            Cmd_Cancel.IsEnabled = true;

            Cmd_AddOrderPos.IsEnabled = false;
            Cmd_ModifyOrderPos.IsEnabled = false;
            Cmd_DeleteOrderPos.IsEnabled = false;
            Cmd_SaveOrderPos.IsEnabled = true;
            Cmd_CancelOrderPos.IsEnabled = true;
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

            Cmd_AddOrderPos.IsEnabled = true;
            Cmd_ModifyOrderPos.IsEnabled = true;
            Cmd_DeleteOrderPos.IsEnabled = true;
            Cmd_SaveOrderPos.IsEnabled = false;
            Cmd_CancelOrderPos.IsEnabled = false;
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

            Cmd_AddOrderPos.IsEnabled = false;
            Cmd_ModifyOrderPos.IsEnabled = false;
            Cmd_DeleteOrderPos.IsEnabled = false;
            Cmd_SaveOrderPos.IsEnabled = false;
            Cmd_CancelOrderPos.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            SetGUIToViewMode();
        }

        private void Window_Closed(object sender, System.EventArgs e) {
            mainWindow.Show();
        }

        private void Cmd_AddOrder_Click(object sender, RoutedEventArgs e) {

            try {
                BusinessManager.CreateLocalOrder();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToModifyMode();
        }

        private void Cmd_SaveOrder_Click(object sender, RoutedEventArgs e) {

            try {
                BusinessManager.SaveModifiedOrder(Orders);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }
            SetGUIToViewMode();
        }

        private void Cmd_ModifyOrder_Click(object sender, RoutedEventArgs e) {

            int index = OrderDataGrid.SelectedIndex;

            try {
                BusinessManager.ModifySelected(Orders, index);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            if (index != -1) {
                SetGUIToModifyMode();
            }
        }

        private void Cmd_DeleteOrder_Click(object sender, RoutedEventArgs e) {
            int index = OrderDataGrid.SelectedIndex;

            try {
                BusinessManager.DeleteSelectedOrder(index);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }
        }

        private void Cmd_Cancel_Click(object sender, RoutedEventArgs e) {


            SetGUIToViewMode();
        }

        private void Cmd_SearchOrders_Click(object sender, RoutedEventArgs e) {

            if (Cmd_SearchOrders.Content == "Reset") {
                SetGUIToViewMode();
                try
                {
                    BusinessManager.LoadAllOrdersFromDb();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                    return;
                }

                Cmd_SearchOrders.Content = "Search";
                Txt_SearchOrders.Clear();
            }
            else {
                string searchText = Txt_SearchOrders.Text;
                if (searchText != String.Empty)
                {
                    SetGUIToFullViewMode();
                    try
                    {
                        BusinessManager.FilterOrder(searchText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                        return;
                    }
                }
                else
                {
                    SetGUIToViewMode();
                    try
                    {
                        BusinessManager.LoadAllOrdersFromDb();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                        return;
                    }
                }

                Cmd_SearchOrders.Content = "Reset";
            }
        }

        private void Cmd_SaveOrderPos_Click(object sender, RoutedEventArgs e) {

        }

        private void OrderDataGrid_BeginningEdit(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e) {
            _orderDataGridChef.BlockReadOnlyRows(e);
        }

        private void OrderPositionDataGrid_BeginningEdit(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e) {
            _orderPositionDataGridChef.BlockReadOnlyRows(e);
        }

        private void Cmd_AddOrderPos_Click(object sender, RoutedEventArgs e) {

        }

        private void OrderDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            try {
                Order selectedOrder = (Order)OrderDataGrid.SelectedItem;
                if (selectedOrder != null)
                {
                    BusinessManager.LoadOrderPositionsForSpecificOrder(selectedOrder.Id);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
