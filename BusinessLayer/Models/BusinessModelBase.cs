using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class BusinessModelBase : BindableBase
    {
        //used for wpf datagrid manipulation
        private bool readOnly;
        public bool ReadOnly
        {
            get => readOnly;
            set => Set(ref readOnly, value);
        }

        private int id;
        public int Id 
        { 
            get => id;
            set => Set(ref id, value);
        }


        public BusinessModelBase()
        {
            readOnly = true;
        }
    }
}
