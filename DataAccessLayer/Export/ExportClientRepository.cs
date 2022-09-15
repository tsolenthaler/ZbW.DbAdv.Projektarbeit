﻿using DataAccessLayer.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Customer;
using DataAccessLayer.Invoice;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Export
{
    public class ExportClientRepository : IExportClientRepository
    {
        public ExportClientDTO[] GetAllCustomersByValidDate(DateTime date)
        {
            DateTime startDate2 = new DateTime(date.Year, date.Month, date.Day);

            string startDateFormat = startDate2.ToString("s");

            List<ExportClientDTO> result = new List<ExportClientDTO>();

            using var context = new SetupDB();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT c.clientnr as customerNr, c.email as email, c.password as password, c.website as website, c.Firstname as Firstname, c.Lastname as Lastname, a.Street as Street, a.StreetNo as StreetNo, a.Plz as Plz FROM [AuftragsverwaltungHistory].[dbo].[Customer] FOR SYSTEM_TIME ALL as c LEFT JOIN [AuftragsverwaltungHistory].[dbo].[Addresses] FOR SYSTEM_TIME ALL as a on c.addressid = a.id WHERE c.ValidFrom <= '{startDateFormat}' and c.ValidTo >= '{startDateFormat}' AND a.ValidFrom <= '{startDateFormat}' and a.ValidTo >= '{startDateFormat}'";

            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ExportClientDTO export = new ExportClientDTO()
                {
                    customerNr = Convert.ToString(reader["customerNr"]),
                    firstname = Convert.ToString(reader["Firstname"]),
                    lastname = Convert.ToString(reader["Lastname"]),
                    address = new ExportClientDTOAddress()
                    {
                        street = Convert.ToString(reader["Street"]),
                        streetno = Convert.ToString(reader["StreetNo"]),
                        postalCode = Convert.ToString(reader["Plz"]),
                    },
                    email = Convert.ToString(reader["email"]),
                    website = Convert.ToString(reader["website"]),
                    password = Convert.ToString(reader["password"]),
                };

                result.Add(export);
            }

            context.Database.CloseConnection();
            return result.ToArray();
        }
    }
}