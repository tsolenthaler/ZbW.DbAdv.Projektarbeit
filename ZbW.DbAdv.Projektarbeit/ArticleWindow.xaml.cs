using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für ArticleWindow.xaml
    /// </summary>
    public partial class ArticleWindow : Window
    {
        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>();

        public ObservableCollection<ArticleGroup> ArticleGroups { get; set; } =
            new ObservableCollection<ArticleGroup>();

        public ArticleWindow()
        {
            InitializeComponent();
        }

        private void SetGUIToModifyMode() {
            Cmd_AddArticle.IsEnabled = false;
            Cmd_ModifyArticle.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveArticle.IsEnabled = true;
            Cmd_Cancel.IsEnabled = true;
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "view mode"
        ///</summary>
        private void SetGUIToViewMode() {
            Cmd_AddArticle.IsEnabled = true;
            Cmd_ModifyArticle.IsEnabled = true;
            Cmd_Delete.IsEnabled = true;
            Cmd_SaveArticle.IsEnabled = false;
            Cmd_Cancel.IsEnabled = false;
        }

        ///<summary>
        ///Deactivates buttons and textfields for GUI "full view mode", applicable when searching content
        ///</summary>
        private void SetGUIToFullViewMode() {
            Cmd_AddArticle.IsEnabled = false;
            Cmd_ModifyArticle.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveArticle.IsEnabled = false;
            Cmd_Cancel.IsEnabled = false;
        }

        private void Cmd_AddArticle_Click(object sender, RoutedEventArgs e) {


            SetGUIToModifyMode();
        }

        private void Cmd_SaveArticle_Click(object sender, RoutedEventArgs e) {
          
            
            SetGUIToViewMode();
        }

        private void Cmd_ModifyArticle_Click(object sender, RoutedEventArgs e) {
           
            
            SetGUIToModifyMode();
        }

        private void Cmd_Delete_Click(object sender, RoutedEventArgs e) {

        }

        private void Cmd_Cancel_Click(object sender, RoutedEventArgs e) {
            
            
            SetGUIToViewMode();
        }

        private void Cmd_SearchArticle_Click(object sender, RoutedEventArgs e) {
            
            // if
            SetGUIToFullViewMode();

            // else
            SetGUIToViewMode();
        }
    }
}
