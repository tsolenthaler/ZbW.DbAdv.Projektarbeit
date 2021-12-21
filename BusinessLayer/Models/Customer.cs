using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BusinessLayer
{
    public class Customer
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string FullName
        {
            get => Firstname + Lastname;
        }
        public Address Address { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        
        //to be defined exactly how passwords are handled
        public HashCode Password { get; set; }

        public Customer()
        {

        }
    }
}