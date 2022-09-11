using DataAccessLayer.Export;
using DataAccessLayer.RepositoryBase;
using DataAccessLayer.Customer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessLayer.Models
{
    [XmlRoot("Kunden")]
    public class Export
    {
        private ObservableCollection<Client> clients;

        public Export()
        {

        }
        public Export(ObservableCollection<Client> clients)
        {
            this.clients = clients;
        }
        [XmlElement("Kunde")]
        public ObservableCollection<Client> Clients { get { return clients; } } 
    }

    [XmlRoot("Kunde")]
    public class Client
    {
        [XmlAttribute("CustomerNr")]
        public string customerNr { get; set; }
        [XmlElement("Company")]
        public string company { get; set; }
        [XmlElement("Name")]
        public string name { get; set; }
        [XmlElement("Address")]
        public ClientAddress address { get; set; }
        [XmlElement("EMail")]
        public string email { get; set; }
        [XmlElement("Website")]
        public string website { get; set; }
        [XmlElement("Password")]
        public string password { get; set; }

        public Client()
        {

        }
        public Client(ExportClientDTO export)
        {
            this.customerNr = export.customerNr;
            this.company = export.company;
            this.name = export.firstname + " " + export.lastname;
            this.address = new ClientAddress(export.address);
            this.email = export.email;
            this.website = export.website;
            this.password = HashString(export.password);
        }

        public CustomerDTO ClienttoCustomer(CustomerDTO customer)
        {
            if(customer == null)
            {
                customer = new CustomerDTO();
            }
            customer.Clientnr = this.customerNr;
            string[] name = this.name.Split(' ');
            customer.Firstname = name[0];
            customer.Lastname = name[1];
            customer.EMail = this.email;
            customer.Website = this.website;
            customer.Password = this.password;
            customer.Company = this.company;
            AddressDTO address = new AddressDTO();
            string[] street = this.address.street.Split(' ');
            address.Street = street[0];
            address.StreetNo = street[1];
            address.Plz = this.address.postalCode;
            address.City = this.address.city;
            customer.Address = address;

            return customer;
        }

        static string HashString(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }

    public class ClientAddress
    {
        [XmlElement("Street")]
        public string street { get; set; }
        [XmlElement("PostalCode")]
        public string postalCode { get; set; }
        [XmlElement("City")]
        public string city { get; set; }

        public ClientAddress()
        {

        }

        public ClientAddress(ExportClientDTOAddress address)
        {
            this.street = address.street + " " + address.streetno;
            this.postalCode = address.postalCode;
            this.city = address.city;
        }
    }
}
