using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using DataAccessLayer.Models;
using Microsoft.Extensions.Primitives;

namespace BusinessLayer.Models
{
    public class Customer : BusinessModelBase
    {
        private string lastName;
        public string LastName
        {
            get => lastName;
            set => Set(ref lastName, value);
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => Set(ref firstName, value);
        }

        private Address address;
        public Address Address
        {
            get => address;
            set => Set(ref address, value);
        }

        private string email;
        public string EMail 
        {
            get => email;
            set => Set(ref email, value);
        }

        private string website;
        public string Website 
        {
            get => website;
            set => Set(ref website, value);
        }

        //to be defined exactly how passwords are handled
        private string password;
        private Customer? customerDTO;

        public string Password 
        {
            get => password;
            set => Set(ref password, value);
        }

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

        public Customer(Customer? customerDTO) {
            this.customerDTO = customerDTO;
        }

        public override string ToString()
        {
            return Id + "| " + FirstName + " " + LastName;
        }
    }
}