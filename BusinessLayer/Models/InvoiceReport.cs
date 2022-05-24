using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class InvoiceReport : BusinessModelBase
    {
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => Set(ref date, value);
        }

        private double netto;
        public double Netto
        {
            get => netto;
            set => Set(ref netto, value);
        }

        private double brutto;
        public double Brutto
        {
            get => brutto;
            set => Set(ref brutto, value);
        }
        public int? CustomerId { get; }
        public string? Name { get; }
        public string? Street { get; }
        public string? StreetNo { get; }
        public string? Plz { get; }
        public string? City { get; }
        public string? Countryname { get; }

        public InvoiceReport()
        {

        }

        public InvoiceReport(InvoiceReportDTO invoiceReportDto)
        {
            
            CustomerId = invoiceReportDto.CustomerId;
            Name = invoiceReportDto.Name;
            Street = invoiceReportDto.Street;
            StreetNo = invoiceReportDto.StreetNo;
            Plz = invoiceReportDto.Plz;
            City = invoiceReportDto.City;
            Countryname = invoiceReportDto.Countryname;
            Id = invoiceReportDto.Id;
            Date = (DateTime)invoiceReportDto.Date;
            Netto = (Double)invoiceReportDto.Netto;
            Brutto = (Double)invoiceReportDto.Brutto;
        }

        //private Customer? customerDTO;
        //public Customer(Customer? customerDTO) {
        //    this.customerDTO = customerDTO;
        //}

        public InvoiceReportDTO ToInvoiceReportDto()
        {
            InvoiceReportDTO invoiceReportDto = new InvoiceReportDTO();
            invoiceReportDto.Id = Id;
            invoiceReportDto.Date = Date;
            invoiceReportDto.Netto = Netto;
            invoiceReportDto.Brutto = Brutto;
            invoiceReportDto.CustomerId = CustomerId;
            invoiceReportDto.Name = Name;
            invoiceReportDto.Street = Street;
            invoiceReportDto.StreetNo = StreetNo;
            return invoiceReportDto;
        }
    }
}
