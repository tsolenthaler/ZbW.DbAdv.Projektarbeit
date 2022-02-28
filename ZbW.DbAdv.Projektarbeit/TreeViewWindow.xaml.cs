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
using BusinessLayer.Models;
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
            List<ArticleGroup> articleGroups = BusinessManager.GetArticleGroupsRecursiveCte();

            TreeViewNode root = new TreeViewNode() { Title = "ArticleGroups", Id=0};

            foreach (ArticleGroup articleGroup in articleGroups)
            {
                if (articleGroup.ParentArticleGroupId == 0)
                {
                    root.Items.Add(new TreeViewNode(){Title = articleGroup.Name, Id = articleGroup.Id});
                }
                else
                {
                    foreach (TreeViewNode node in root.Items)
                    {
                        if (node.Id == articleGroup.ParentArticleGroupId)
                        {
                            node.Items.Add(new TreeViewNode() { Title = articleGroup.Name, Id = articleGroup.Id });
                        }
                        else
                        {
                            //MessageBox.Show("Parent ArticleGroup not found! Child Node: " + articleGroup.Name);
                        }
                    }
                }
            }
            TreeViewArticleGroups.Items.Add(root);
        }

    }
}
