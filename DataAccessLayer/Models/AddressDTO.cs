using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int StreetNo { get; set; }
        public string City { get; set; }
        public string Plz { get; set; }
    }
}