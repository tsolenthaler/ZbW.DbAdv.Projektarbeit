using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Customer
{
    public class CustomerDTO : TEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        public string Clientnr { get; set; }
        public string Company { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public int AddressId { get; set; }
        public virtual AddressDTO Address { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        //to be defined exactly how passwords are handled
        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0
        public string Password { get; set; }
    }
}