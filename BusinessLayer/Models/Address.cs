using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Address : BusinessModelBase
    {
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string Plz { get; set; }

        public Address(AddressDTO addressDto)
        {
            Id = addressDto.Id;
            Street = addressDto.Street;
            StreetNo = addressDto.StreetNo;
            City = addressDto.City;
            Plz = addressDto.Plz;
        }

        public Address()
        {

        }
    }
}