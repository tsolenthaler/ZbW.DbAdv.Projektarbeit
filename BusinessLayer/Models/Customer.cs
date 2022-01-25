using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Customer
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

        public Customer()
        {

        }
        public Customer(CustomerDTO customerDto)
        {
            //implement assignment after DTO is defined
        }

        public override string ToString()
        {
            return Id + "| " + FirstName + " " + LastName;
        }
    }
}