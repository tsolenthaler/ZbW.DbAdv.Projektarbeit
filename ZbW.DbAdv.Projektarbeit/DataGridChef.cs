using BusinessLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace PresentationLayer
{
    public class DataGridChef
    {
        private readonly DataGrid _dataGrid;
        private readonly List<int> _comboBoxColumnIndices;
        private readonly List<int> _datePickerColumnIndices;
        
        public SolidColorBrush ReadOnlyFieldColor = new SolidColorBrush(Colors.LightGray);
        //private static readonly SolidColorBrush _mandatoryFieldColor = new SolidColorBrush(Colors.Thistle);
        //private static readonly SolidColorBrush _modifiableFieldColor = new SolidColorBrush(Colors.White);
        //public DataGridCell? SelectedCell { get; set; }

        public DataGridChef(DataGrid dataGrid, List<int> comboBoxColumnIndices, List<int> datePickerColumnIndices)
        {
            _dataGrid = dataGrid;
            _comboBoxColumnIndices = comboBoxColumnIndices;
            _datePickerColumnIndices = datePickerColumnIndices;
        }

        public void BlockReadOnlyRows(DataGridBeginningEditEventArgs e)
        {
            if (((Customer)e.Row.Item).ReadOnly)
            {
                e.Cancel = true;
            }
        }


    }
}
