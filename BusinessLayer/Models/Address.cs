﻿using DataAccessLayer.Customer;
using System.Text.Json.Serialization;

namespace BusinessLayer.Models
{
    public class Address : BusinessModelBase
    {
        public enum Country
        {
            Schweiz,
            Lichtenstein,
            Deutschland,
            Österreich,
            Italien,
            Frankreich
        }
        private string street;
        [JsonIgnore]
        public string Street
        {
            get => street;
            set => Set(ref street, value);
        }

        private string streetno;
        [JsonIgnore]
        public string StreetNo
        {
            get => streetno;
            set => Set(ref streetno, value);
        }
        [JsonPropertyName("street")]
        public string streetWithNo
        {
            get => street + " " + streetno;
        }

        private string city;
        [JsonIgnore]
        public string City
        {
            get => city;
            set => Set(ref city, value);
        }

        private string plz;
        [JsonPropertyName("postalCode")]
        public string Plz
        {
            get => plz;
            set => Set(ref plz, value);
        }

        private Country countryname;
        [JsonIgnore]
        public Country Countryname
        {
            get => countryname;
            set => Set(ref countryname, value);
        }

        public Address(AddressDTO addressDto)
        {
            Id = addressDto.Id;
            Street = addressDto.Street;
            StreetNo = addressDto.StreetNo;
            City = addressDto.City;
            Plz = addressDto.Plz;
            Countryname = (Country)addressDto.Countryname;
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
            addressDto.Countryname = (AddressDTO.Country)Countryname;
            return addressDto;
        }
    }
}