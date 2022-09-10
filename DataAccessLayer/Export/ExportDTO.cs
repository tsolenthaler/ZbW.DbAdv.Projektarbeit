using DataAccessLayer.Customer;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Export
{
    public class ExportDTO : TEntity
    {
        public string customerNr { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string password { get; set; }
    }
}
