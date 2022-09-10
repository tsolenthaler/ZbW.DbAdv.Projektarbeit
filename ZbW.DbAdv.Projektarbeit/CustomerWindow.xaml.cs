using BusinessLayer;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly DataGridChef _dataGridChef;

        //no ComboBox or DatePicker necessary for Customer Window
        private readonly List<int> _comboBoxColumnIndices = new List<int>() { };
        private readonly List<int> _datePickerColumnIndices = new List<int>() { };

        public ObservableCollection<Customer> Customers
        {
            get => mainWindow.BusinessManager.Customers;
        }

        public readonly MainWindow mainWindow;

        public BusinessManager BusinessManager
        {
            get => mainWindow.BusinessManager;
        }


        public CustomerWindow(MainWindow mainWindow)
        {
            DataContext = this;
            this.mainWindow = mainWindow;

            InitializeComponent();

            _dataGridChef = new DataGridChef(CustomerDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            Resources["readOnlyColor"] = _dataGridChef.ReadOnlyFieldColor;

            try
            {
                BusinessManager.LoadAllCustomersFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetGUIToViewMode();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Show();
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "modify mode"
        ///</summary>
        private void SetGUIToModifyMode()
        {
            Cmd_AddCustomer.IsEnabled = false;
            Cmd_ModifyCustomer.IsEnabled = false;
            Cmd_DeleteCustomer.IsEnabled = false;
            Cmd_SaveCustomer.IsEnabled = true;
            Cmd_Cancel.IsEnabled = true;
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "view mode"
        ///</summary>
        private void SetGUIToViewMode()
        {
            Cmd_AddCustomer.IsEnabled = true;
            Cmd_ModifyCustomer.IsEnabled = true;
            Cmd_DeleteCustomer.IsEnabled = true;
            Cmd_SaveCustomer.IsEnabled = false;
            Cmd_Cancel.IsEnabled = false;
        }

        ///<summary>
        ///Deactivates buttons and textfields for GUI "full view mode", applicable when searching content
        ///</summary>
        private void SetGUIToFullViewMode()
        {
            Cmd_AddCustomer.IsEnabled = false;
            Cmd_ModifyCustomer.IsEnabled = false;
            Cmd_DeleteCustomer.IsEnabled = false;
            Cmd_SaveCustomer.IsEnabled = false;
            Cmd_Cancel.IsEnabled = false;
        }

        private void CustomerDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _dataGridChef.BlockReadOnlyRows(e);
        }

        private void Cmd_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                BusinessManager.CreateLocalCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToModifyMode();
        }

        private void Cmd_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessManager.CancelModificationCustomer(Customers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToViewMode();
        }



        private void Cmd_ModifyCustomer_Click(object sender, RoutedEventArgs e)
        {
            int index = CustomerDataGrid.SelectedIndex;

            try
            {
                BusinessManager.ModifySelected(Customers, index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            if (index != -1)
            {
                SetGUIToModifyMode();
            }

        }

        private void Cmd_SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessManager.SaveModifiedCustomer(Customers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToViewMode();
        }

        private void Cmd_DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            int index = CustomerDataGrid.SelectedIndex;

            try
            {
                BusinessManager.DeleteSelectedCustomer(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

        }

        private void Cmd_SearchCustomers_Click(object sender, RoutedEventArgs e)
        {
            if (Cmd_SearchCustomers.Content == "Reset")
            {
                SetGUIToViewMode();
                try
                {
                    BusinessManager.LoadAllCustomersFromDb();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                    return;
                }

                Cmd_SearchCustomers.Content = "Search";
                Txt_SearchCustomer.Clear();
            }
            else
            {
                string searchText = Txt_SearchCustomer.Text;
                if (searchText != String.Empty)
                {
                    SetGUIToFullViewMode();
                    try
                    {
                        BusinessManager.FilterCustomers(searchText);
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
                        BusinessManager.LoadAllCustomersFromDb();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                        return;
                    }
                }

                Cmd_SearchCustomers.Content = "Reset";
            }
        }

        private void Cmd_ExportJson_Click(object sender, RoutedEventArgs e)
        {

            DateTime? startDate = DatePickerDate.SelectedDate;
            if (startDate.HasValue)
            {
                try
                {
                    BusinessManager.SerialzationJSON((DateTime)startDate);
                    MessageBox.Show("Export JSON erfolgreich!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Bitte Datum wählen!");
            }
            
        }

        private void Cmd_ExportXml_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = DatePickerDate.SelectedDate;
            if (startDate.HasValue)
            {
                try
                {
                    BusinessManager.SerialzationXML((DateTime)startDate);
                    MessageBox.Show("Export XML erfolgreich!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Bitte Datum wählen!");
            }
        }

        private void Cmd_ImportJson_Click(object sender, RoutedEventArgs e)
        {
            BusinessManager.DeserialzationJSON();
            /*
            try
            {
                BusinessManager.DeserialzationJSON();
                MessageBox.Show("Import Json erfolgreich!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }*/
        }

        private void Cmd_ImportXml_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Import Xml");
        }

        private void Cmd_SelectedDate(object sender, SelectionChangedEventArgs e)
        {
            DateTime? startDate = DatePickerDate.SelectedDate;
        }
    }
}
