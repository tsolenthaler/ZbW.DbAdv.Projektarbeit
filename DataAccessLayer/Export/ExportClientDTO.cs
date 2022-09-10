using DataAccessLayer.Customer;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Export
{
    public class ExportClientDTO : TEntity
    {
        public string customerNr { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public ExportClientDTOAddress address { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string password { get; set; }
    }

    public class ExportClientDTOAddress : TEntity
    {
        public string street { get; set; }
        public string streetno { get; set; }
        public string postalCode { get; set; }
    }
}
