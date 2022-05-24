using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using DataAccessLayer.Models;
using Microsoft.Extensions.Primitives;

namespace BusinessLayer.Models
{
    public class Invoice : BusinessModelBase
    {
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => Set(ref date, value);
        }

        private Customer customer = new Customer();
        public Customer Customer
        {
            get => customer;
            set => Set(ref customer, value);
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

        public Invoice()
        {

        }

        public Invoice(InvoiceDTO invoiceDto)
        {
            Id = invoiceDto.Id;
            Date = (DateTime)invoiceDto.Date;
            Netto = invoiceDto.Netto;
            Brutto = invoiceDto.Brutto;

            Customer = new Customer(invoiceDto.Customer);
        }

        //private Customer? customerDTO;
        //public Customer(Customer? customerDTO) {
        //    this.customerDTO = customerDTO;
        //}

        public override string ToString()
        {
            return Id + "| " + Customer.FirstName + " " + Customer.LastName;
        }

        public InvoiceDTO ToInvoiceDto()
        {
            InvoiceDTO invoiceDto = new InvoiceDTO();
            invoiceDto.Id = Id;
            invoiceDto.Date = Date;
            invoiceDto.Netto = Netto;
            invoiceDto.Brutto = Brutto;
            invoiceDto.Customer = Customer.ToCustomerDto();
            invoiceDto.CustomerId = Customer.Id;
            return invoiceDto;
        }
    }
}