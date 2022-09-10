using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Customer;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Export
{
    public interface IExportClientRepository
    {
        public ExportClientDTO[] GetAllCustomersByValidDate(DateTime date);
    }
}
