using DataAccessLayer.Models;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Customer
{
    public class AddressDTO : TEntity
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