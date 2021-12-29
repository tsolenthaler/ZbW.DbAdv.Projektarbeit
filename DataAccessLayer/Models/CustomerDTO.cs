using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public virtual AddressDTO Address { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        //to be defined exactly how passwords are handled
        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0
        public HashCode Password { get; set; }

    }
}