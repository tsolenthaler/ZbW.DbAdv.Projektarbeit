using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ZbW.DbAdv.Projektarbeit;
using BusinessLayer;
using BusinessLayer.Models;
using System.Collections.ObjectModel;

namespace PresentationLayer {
    /// <summary>
    /// Interaktionslogik für InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {

        private readonly DataGridChef _dataGridChef;

        //no ComboBox or DatePicker necessary for Customer Window
        private readonly List<int> _comboBoxColumnIndices = new List<int>() { };
        private readonly List<int> _datePickerColumnIndices = new List<int>() { };

        public ObservableCollection<Invoice> Invoices { get => mainWindow.BusinessManager.Invoices; }

        public readonly MainWindow mainWindow;
        public BusinessManager BusinessManager { get => mainWindow.BusinessManager; }

        public InvoiceWindow(MainWindow mainWindow)
        {
            DataContext = this;
            this.mainWindow = mainWindow;

            InitializeComponent();

            _dataGridChef = new DataGridChef(InvoiceDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            Resources["readOnlyColor"] = _dataGridChef.ReadOnlyFieldColor;

            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            DatePickerStartDate.SelectedDate = startDate;
            DatePickerEndDate.SelectedDate = endDate;

            BusinessManager.LoadAllInvoicesFromDbbyDate(startDate, endDate);
            /*try {
                BusinessManager.LoadAllInvoicesFromDb();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
            }*/
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }

        private void InvoiceDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _dataGridChef.BlockReadOnlyRows(e);
        }

        private void Cmd_SelectedDate(object sender, SelectionChangedEventArgs e)
        {
            DateTime? startDate = DatePickerStartDate.SelectedDate;
            DateTime? endDate = DatePickerEndDate.SelectedDate;

            if (startDate != null && endDate != null)
            {
                BusinessManager.LoadAllInvoicesFromDbbyDate((DateTime)startDate, (DateTime)endDate);
                /*try
                {
                    BusinessManager.LoadAllInvoicesFromDbbyDate((DateTime)searchDate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                    return;
                }*/
            }
        }
    }
}
