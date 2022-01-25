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
    }
}
