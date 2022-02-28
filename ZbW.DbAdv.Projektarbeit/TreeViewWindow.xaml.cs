using System;
using System.Collections.Generic;
using System.Linq;
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
using PresentationLayer;
using BusinessLayer;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for TreeViewWindow.xaml
    /// </summary>
    public partial class TreeViewWindow : Window
    {
        public readonly MainWindow mainWindow;
        public readonly ArticleWindow articleWindow;
        public BusinessManager BusinessManager { get => mainWindow.BusinessManager; }

        public TreeViewWindow(MainWindow mainWindow, ArticleWindow articleWindow)
        {
            DataContext = this;
            this.mainWindow = mainWindow;
            this.articleWindow = articleWindow;

            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            articleWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BusinessManager.GetArticleGroupsRecursiveCte();
        }
    }
}
