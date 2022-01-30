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
    public class Customer : BusinessModelBase
    {
        public string LastName { get; set; }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => Set(ref firstName, value);
        }
        public Address Address { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        
        //to be defined exactly how passwords are handled
        public string Password { get; set; }

        public Customer()
        {

        }
        public Customer(CustomerDTO customerDto)
        {
            Id = customerDto.Id;
            FirstName = customerDto.Firstname;
            LastName = customerDto.Lastname;
            EMail = customerDto.EMail;
            Website = customerDto.Website;
            Password = customerDto.Password;

            Address = new Address(customerDto.Address);
        }

        public override string ToString()
        {
            return Id + "| " + FirstName + " " + LastName;
        }
    }
}