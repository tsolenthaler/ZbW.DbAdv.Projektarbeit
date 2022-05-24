using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class InvoiceReportDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Street { get; set; }
        public string? StreetNo { get; set; }
        public string? Plz { get; set; }
        public string? City { get; set; }
        public string? Countryname { get; set; }
        public double? Netto { get; set; }
        public double? Brutto { get; set; }
    }
}
