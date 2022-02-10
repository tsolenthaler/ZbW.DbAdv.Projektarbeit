using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Address : BusinessModelBase
    {
        private string street = String.Empty;
        public string Street
        {
            get => street;
            set => Set(ref street, value);
        }

        private string streetno = String.Empty;
        public string StreetNo
        {
            get => streetno;
            set => Set(ref streetno, value);
        }

        private string city = String.Empty;
        public string City
        {
            get => city;
            set => Set(ref city, value);
        }

        private string plz = String.Empty;
        public string Plz
        {
            get => plz;
            set => Set(ref plz, value);
        }

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

        public AddressDTO ToAddressDto()
        {
            AddressDTO addressDto = new AddressDTO();
            addressDto.Id = Id;
            addressDto.Street = Street;
            addressDto.StreetNo = StreetNo;
            addressDto.City = City;
            addressDto.Plz = Plz;
            return addressDto;
        }
    }
}