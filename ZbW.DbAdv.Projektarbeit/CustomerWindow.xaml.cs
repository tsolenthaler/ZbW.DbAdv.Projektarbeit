using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public ObservableCollection<Customer> Customers { get => mainWindow.BusinessManager.Customers; }

        public readonly MainWindow mainWindow;

        public BusinessManager BusinessManager { get => mainWindow.BusinessManager; }
        

        public CustomerWindow(MainWindow mainWindow)
        {
            DataContext = this;
            this.mainWindow = mainWindow;

            InitializeComponent();
            
            _dataGridChef = new DataGridChef(CustomerDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            Resources["readOnlyColor"] = _dataGridChef.ReadOnlyFieldColor;

            BusinessManager.LoadAllCustomersFromDb();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Show();
        }

        private void CustomerDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _dataGridChef.BlockReadOnlyRows(e);
        }

        private void Cmd_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            BusinessManager.CreateLocalCustomer();
            SetGUIToFullViewMode();
        }

        private void Cmd_Cancel_Click(object sender, RoutedEventArgs e)
        {
            BusinessManager.CancelModification(Customers);
            SetGUIToViewMode();
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "modify mode"
        ///</summary>
        private void SetGUIToModifyMode()
        {
            //enable / disable buttons
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "view mode"
        ///</summary>
        private void SetGUIToViewMode()
        {
            //enable / disable buttons
        }

        ///<summary>
        ///Deactivates buttons and textfields for GUI "full view mode", applicable when searching content
        ///</summary>
        private void SetGUIToFullViewMode()
        {
            //enable / disable buttons
        }

        private void Cmd_ModifyCustomer_Click(object sender, RoutedEventArgs e)
        {
            int index = CustomerDataGrid.SelectedIndex;
            BusinessManager.ModifySelected(Customers, index);
            SetGUIToModifyMode();
        }

        private void Cmd_SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            BusinessManager.SaveModified(Customers);
            SetGUIToViewMode();
        }

        private void Cmd_DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            int index = CustomerDataGrid.SelectedIndex;
            BusinessManager.DeleteSelected(Customers, index);
        }

        private void Cmd_SearchCustomers_Click(object sender, RoutedEventArgs e)
        {
            string searchText = Txt_SearchCustomer.Text;

            if (searchText != String.Empty)
            {
                //update customer list based on search result
            }
        }
    }
}
