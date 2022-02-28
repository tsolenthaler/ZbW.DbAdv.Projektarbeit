using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class TreeViewNode
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public ObservableCollection<TreeViewNode> Items { get; set; }

        public TreeViewNode()
        {
            this.Items = new ObservableCollection<TreeViewNode>();
        }
    }
}
