using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PresentationLayer
{
    public class CustomerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            Customer customer = (value as BindingGroup).Items[0] as Customer;
            string cliennrPattern = @"^CU\d{5}$";
            if (!Regex.IsMatch(customer.Clientnr, cliennrPattern))
            {
                return new ValidationResult(false,
                    "Clientnr muss mit 'CU' anfangen und 5 Zahlen enthalten");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
            string emailPattern = @"^((?:[A-Za-z0-9!#$%&'*+\\-\\/=?^_`{|}~]|(?<=^|\\.)'|'(?=$|\\.|@)|(?<='.*)[ .](?=.*')|(?<!\\.)\\.){1,64})(@)((?:[A-Za-z0-9.\\-])*(?:[A-Za-z0-9])\\.(?:[A-Za-z0-9]){2,})$";
            if (!Regex.IsMatch(customer.EMail, emailPattern))
            {
                return new ValidationResult(false,
                    "Die E-Mailadresse ist ungültig.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
