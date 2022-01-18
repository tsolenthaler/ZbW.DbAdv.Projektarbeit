using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int StreetNo { get; set; }
        public string City { get; set; }
        public string Plz { get; set; }

        public Address(AddressDTO addressDto)
        {
            //implement assignment after DTO is defined
        }
    }
}