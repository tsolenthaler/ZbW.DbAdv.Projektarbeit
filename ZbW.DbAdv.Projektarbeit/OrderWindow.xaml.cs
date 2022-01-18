using System.Collections.Generic;
using System.Windows;
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

        public OrderWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            dataGridChef = new DataGridChef(orderDataGrid, comboBoxColumnIndices, datePickerColumnIndices);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridChef.InitialTableDefinition();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }
    }
}
