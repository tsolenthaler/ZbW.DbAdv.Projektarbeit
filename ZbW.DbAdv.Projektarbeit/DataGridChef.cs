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
        private DataGrid dataGrid;
        private List<int> comboBoxColumnIndices;
        private List<int> datePickerColumnIndices;
        
        private static readonly SolidColorBrush readOnlyFieldColor = new SolidColorBrush(Colors.LightGray);
        private static readonly SolidColorBrush mandatoryFieldColor = new SolidColorBrush(Colors.Thistle);
        private static readonly SolidColorBrush modifiableFieldColor = new SolidColorBrush(Colors.White);
        public DataGridCell? SelectedCell { get; set; }

        public DataGridChef(DataGrid dataGrid, DataTable dataTable, List<int> comboBoxColumnIndices, List<int> datePickerColumnIndices)
        {
            this.dataGrid = dataGrid;
            this.comboBoxColumnIndices = comboBoxColumnIndices;
            this.datePickerColumnIndices = datePickerColumnIndices;
        }

        ///<summary>
        ///Initial Definition of the tables, set all to read-only and set corresponding colors
        ///</summary>
        public void InitialTableDefinition()
        {
            dataGrid.Background = readOnlyFieldColor;
            FillDataTable();

            var rows = GetDataGridRows(dataGrid);

            /*
            for (int i = 0; i < GetData; i++)
            {
                MyDataGridView.Rows[i].ReadOnly = true;
                MyDataGridView.Rows[i].DefaultCellStyle.BackColor = readOnlyFieldColor;
            }

            DataGrid.AllowUserToAddRows = false;
            PreventSortingOfAllColumns();
            */
        }

        public void FillDataTable()
        {

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
