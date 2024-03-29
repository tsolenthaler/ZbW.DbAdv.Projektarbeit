﻿using System;
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
            //Get all ArticleGroups recursive with their parent Id
            List<ArticleGroup> articleGroups = BusinessManager.GetArticleGroupsRecursiveCte();
            
            //Convert to TreeViewNode
            List<TreeViewNode> nodes = new List<TreeViewNode>();          
            foreach(ArticleGroup articleGroup in articleGroups)
            {
                nodes.Add(new TreeViewNode() { Id = articleGroup.Id, Title = articleGroup.Name, ParentId=articleGroup.ParentArticleGroupId });
            }

            TreeViewNode root = new TreeViewNode() { Title = "ArticleGroups", Id = 0 };
            int parentNode = 0;
            for(int i=0; i < nodes.Count; i++)
            {
                //add to root node
                if(nodes[i].ParentId == 0)
                {
                    root.Items.Add(nodes[i]);
                }
                else
                {
                    foreach(TreeViewNode node in nodes)
                    {
                        if(nodes[i].ParentId == node.Id)
                        {
                            node.Items.Add(nodes[i]);
                        }
                    }
                }
                
                if(TreeViewArticleGroups.Items.Count == 0)
                {
                    TreeViewArticleGroups.Items.Add(root);
                }     
            }


        }

    }
}
