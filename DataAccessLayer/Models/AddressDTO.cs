using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Models
{
    public class AddressDTO
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
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string Plz { get; set; }
        public Country Countryname { get; set; }
    }
}