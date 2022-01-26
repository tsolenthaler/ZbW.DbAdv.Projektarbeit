using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Customer : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName;
        }
        public Address Address { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        
        //to be defined exactly how passwords are handled
        public HashCode Password { get; set; }

        private bool readOnly;

        //used to color / block rows in WPF DataGrid
        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; NotifyPropertyChanged(); }
        }

        public Customer()
        {
            ReadOnly = true;
        }
        public Customer(CustomerDTO customerDto)
        {
            //implement assignment after DTO is defined
            ReadOnly = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Id + "| " + FirstName + " " + LastName;
        }
    }
}