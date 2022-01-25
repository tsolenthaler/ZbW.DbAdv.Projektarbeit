using System;
using System.Collections;
using System.Collections.Generic;
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
        
        private static readonly SolidColorBrush _readOnlyFieldColor = new SolidColorBrush(Colors.LightGray);
        private static readonly SolidColorBrush _mandatoryFieldColor = new SolidColorBrush(Colors.Thistle);
        private static readonly SolidColorBrush _modifiableFieldColor = new SolidColorBrush(Colors.White);
        public DataGridCell? SelectedCell { get; set; }

        public DataGridChef(DataGrid dataGrid, List<int> comboBoxColumnIndices, List<int> datePickerColumnIndices)
        {
            _dataGrid = dataGrid;
            _comboBoxColumnIndices = comboBoxColumnIndices;
            _datePickerColumnIndices = datePickerColumnIndices;
        }

        ///<summary>
        ///Initial Definition of the tables, set all to read-only and set corresponding colors
        ///</summary>
        public void InitialTableDefinition()
        {
            _dataGrid.Background = _readOnlyFieldColor;

            /*
            var rows = GetDataGridRows(_dataGrid);

            for (int i = 0; i < rows.Count(); i++)
            {
                _dataGrid.Cells
                
                _dataGrid.Rows[i].ReadOnly = true;
                MyDataGridView.Rows[i].DefaultCellStyle.BackColor = readOnlyFieldColor;
            }

            DataGrid.AllowUserToAddRows = false;
            PreventSortingOfAllColumns();
            */
        }


        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
    }
}
