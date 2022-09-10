using DataAccessLayer.Customer;
using System.Text.Json.Serialization;

namespace BusinessLayer.Models
{
    public class Customer : BusinessModelBase
    {
        private string clientnr;
        public string Clientnr
        {
            get => clientnr;
            set => Set(ref clientnr, value);
        }
        private string company;
        public string Company
        {
            get => company;
            set => Set(ref company, value);
        }

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

        public string name
        {
            get => firstName + " " + lastName;
        }

        private Address address = new Address();
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
            Clientnr = customerDto.Clientnr;
            Company = customerDto.Company;
            FirstName = customerDto.Firstname;
            LastName = customerDto.Lastname;
            EMail = customerDto.EMail;
            Website = customerDto.Website;
            Password = customerDto.Password;

            Address = new Address(customerDto.Address);
        }

        //private Customer? customerDTO;
        //public Customer(Customer? customerDTO) {
        //    this.customerDTO = customerDTO;
        //}

        public override string ToString()
        {
            return Id + "| " + FirstName + " " + LastName;
        }

        public CustomerDTO ToCustomerDto()
        {
            CustomerDTO customerDto = new CustomerDTO();
            customerDto.Id = Id;
            customerDto.Clientnr = Clientnr;
            customerDto.Company = Company;
            customerDto.Firstname = FirstName;
            customerDto.Lastname = LastName;
            customerDto.EMail = EMail;
            customerDto.Website = Website;
            customerDto.Password = Password;
            customerDto.Address = Address.ToAddressDto();
            customerDto.AddressId = Address.Id;
            return customerDto;
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
}