using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class SharedWindowFunctions
    {
        public ObservableCollection<string> ArticleGroupsDropDown = new ObservableCollection<string>()
            {"string1", "string2"};
    }
}
