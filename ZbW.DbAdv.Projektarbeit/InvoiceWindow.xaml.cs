﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer {
    /// <summary>
    /// Interaktionslogik für InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window {

        private readonly DataGridChef _dataGridChef;

        //no ComboBox or DatePicker necessary for Customer Window
        private readonly List<int> _comboBoxColumnIndices = new List<int>() { };
        private readonly List<int> _datePickerColumnIndices = new List<int>() { };

        public readonly MainWindow mainWindow;


        public InvoiceWindow(MainWindow mainWindow) {
            DataContext = this;
            this.mainWindow = mainWindow;

            InitializeComponent();

            _dataGridChef = new DataGridChef(InvoiceDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            Resources["readOnlyColor"] = _dataGridChef.ReadOnlyFieldColor;

            try {
                // BusinessManager.LoadAllInvoicesFromDb();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
            }
        }

        private void Window_Closed(object sender, System.EventArgs e) {
            mainWindow.Show();
        }

        private void InvoiceDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e) {
            _dataGridChef.BlockReadOnlyRows(e);
        }
    }
}
