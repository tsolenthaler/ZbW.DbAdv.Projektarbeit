using DataAccessLayer.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Invoice
{
    public interface IInvoiceRepository : IRepositoryBase<InvoiceDTO>
    {
        List<InvoiceReportDTO> GetAllInvoicesbyDate(DateTime startDate, DateTime endDate);

        InvoiceDTO[] GetAllInvoices();

        List<YearEndStatisticDTO> GetYearEndingData();
    }
}
