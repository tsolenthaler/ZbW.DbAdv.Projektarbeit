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
using BusinessLayer;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore.Design;
using ZbW.DbAdv.Projektarbeit;

namespace PresentationLayer
{
    /// <summary>
    /// Interaktionslogik für ArticleWindow.xaml
    /// </summary>
    public partial class ArticleWindow : Window
    {
        private readonly DataGridChef _articleDataGridChef;
        private readonly DataGridChef _articleGroupDataGridChef;

        //no ComboBox or DatePicker necessary for Customer Window
        private readonly List<int> _comboBoxColumnIndices = new List<int>() { };
        private readonly List<int> _datePickerColumnIndices = new List<int>() { };

        public ObservableCollection<Article> Articles { get => mainWindow.BusinessManager.Articles; }
        public ObservableCollection<ArticleGroup> ArticleGroups { get => mainWindow.BusinessManager.ArticleGroups; }

        public readonly MainWindow mainWindow;

        public BusinessManager BusinessManager { get => mainWindow.BusinessManager; }

        public ArticleWindow(MainWindow mainWindow)
        {
            DataContext = this;
            this.mainWindow = mainWindow;

            InitializeComponent();

            _articleDataGridChef = new DataGridChef(ArticleDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            _articleGroupDataGridChef = new DataGridChef(ArticleGroupDataGrid, _comboBoxColumnIndices, _datePickerColumnIndices);
            Resources["readOnlyColor"] = _articleDataGridChef.ReadOnlyFieldColor;

            try
            {
                BusinessManager.LoadAllArticlesFromDb();
                BusinessManager.LoadAllArticleGroupsFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
            }
        }

        private void SetGUIToModifyMode() {
            Cmd_AddArticle.IsEnabled = false;
            Cmd_ModifyArticle.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveArticle.IsEnabled = true;
            Cmd_CancelArticle.IsEnabled = true;
        }

        private void SetGUIToModifyModeArticleGroup() {
            Cmd_AddArticleGroup.IsEnabled = false;
            Cmd_ModifyArticleGroup.IsEnabled = false;
            Cmd_DeleteArticleGroup.IsEnabled = false;
            Cmd_SaveArticleGroup.IsEnabled = true;
            Cmd_CancelArticleGroup.IsEnabled = true;
        }

        ///<summary>
        ///Activates and deactives buttons and textfields for GUI "view mode"
        ///</summary>
        private void SetGUIToViewMode() {
            Cmd_AddArticle.IsEnabled = true;
            Cmd_ModifyArticle.IsEnabled = true;
            Cmd_Delete.IsEnabled = true;
            Cmd_SaveArticle.IsEnabled = false;
            Cmd_CancelArticle.IsEnabled = false;
        }

        private void SetGUIToViewModeArticleGroup()
        {
            Cmd_AddArticleGroup.IsEnabled = true;
            Cmd_ModifyArticleGroup.IsEnabled = true;
            Cmd_DeleteArticleGroup.IsEnabled = true;
            Cmd_SaveArticleGroup.IsEnabled = false;
            Cmd_CancelArticleGroup.IsEnabled = false;
        }

        ///<summary>
        ///Deactivates buttons and textfields for GUI "full view mode", applicable when searching content
        ///</summary>
        private void SetGUIToFullViewMode() {
            Cmd_AddArticle.IsEnabled = false;
            Cmd_ModifyArticle.IsEnabled = false;
            Cmd_Delete.IsEnabled = false;
            Cmd_SaveArticle.IsEnabled = false;
            Cmd_CancelArticle.IsEnabled = false;
        }

        private void SetGUIToFullViewModeArticleGroup()
        {
            Cmd_AddArticleGroup.IsEnabled = false;
            Cmd_ModifyArticleGroup.IsEnabled = false;
            Cmd_DeleteArticleGroup.IsEnabled = false;
            Cmd_SaveArticleGroup.IsEnabled = false;
            Cmd_CancelArticleGroup.IsEnabled = false;
        }


        /* ----------------------------- Article Buttons ------------------------------------------ */
        private void Cmd_AddArticle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessManager.CreateLocalArticle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }
            SetGUIToModifyMode();
        }

        private void Cmd_SaveArticle_Click(object sender, RoutedEventArgs e) {
            try
            {
                BusinessManager.SaveModifiedArticle(Articles);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToViewMode();
        }

        private void Cmd_ModifyArticle_Click(object sender, RoutedEventArgs e) {

            int index = ArticleDataGrid.SelectedIndex;

            try
            {
                BusinessManager.ModifySelected(Articles, index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            if (index != -1)
            {
                SetGUIToModifyMode();
            }
        }

        private void Cmd_DeleteArticle_Click(object sender, RoutedEventArgs e) {

            int index = ArticleDataGrid.SelectedIndex;

            try
            {
                BusinessManager.DeleteSelectedArticle(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }
        }

        private void Cmd_CancelArticle_Click(object sender, RoutedEventArgs e) {
            try {
                BusinessManager.CancelModificationArticle(Articles);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToViewMode();
        }

        private void Cmd_SearchArticle_Click(object sender, RoutedEventArgs e) {

            if (Cmd_SearchArticle.Content == "Reset")
            {
                SetGUIToViewMode();

                try {
                    BusinessManager.LoadAllArticlesFromDb();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                    return;
                }

                Cmd_SearchArticle.Content = "Search";
                Txt_SearchArticle.Clear();
            }
            else
            {
                string searchText = Txt_SearchArticle.Text;
                
                if (searchText != String.Empty) {
                    SetGUIToFullViewMode();

                    try {
                        BusinessManager.FilterArticle(searchText);
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                        return;
                    }
                }
                else
                {
                    SetGUIToViewMode();

                    try {
                        BusinessManager.LoadAllArticlesFromDb();
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                        return;
                    }
                }

                Cmd_SearchArticle.Content = "Reset";
            }

        }
        /* ---------------------------------------------------------------------------------------- */

        /* ----------------------- Article Group Buttons ------------------------------------------ */
        public void Cmd_AddArticleGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessManager.CreateLocalArticleGroup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }
            SetGUIToModifyModeArticleGroup();
        }

        public void Cmd_SaveArticleGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessManager.SaveModifiedArticleGroup(ArticleGroups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToViewModeArticleGroup();
        }

        public void Cmd_ModifyArticleGroup_Click(object sender, RoutedEventArgs e)
        {
            int index = ArticleGroupDataGrid.SelectedIndex;

            try
            {
                BusinessManager.ModifySelected(ArticleGroups, index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            if (index != -1)
            {
                SetGUIToModifyModeArticleGroup();
            }
        }

        public void Cmd_DeleteArticleGroup_Click(object sender, RoutedEventArgs e)
        {
            int index = ArticleGroupDataGrid.SelectedIndex;

            try
            {
                BusinessManager.DeleteSelectedArticleGroup(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }
        }

        private void Cmd_CancelArticleGroup_Click(object sender, RoutedEventArgs e)
        {
            try {
                BusinessManager.CancelModificationArticleGroup(ArticleGroups);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\r\n\r\n Inner Exception: " + ex.InnerException?.Message);
                return;
            }

            SetGUIToViewModeArticleGroup();
        }
        /* ---------------------------------------------------------------------------------------- */

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetGUIToViewMode();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Show();
        }

        private void ArticleDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _articleDataGridChef.BlockReadOnlyRows(e);
        }

        private void ArticleGroupDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            _articleGroupDataGridChef.BlockReadOnlyRows(e);
        }
    }
}
