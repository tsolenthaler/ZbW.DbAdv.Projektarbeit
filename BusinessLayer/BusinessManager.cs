﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using BusinessLayer.Models;
using Castle.Core.Resource;
using DataAccessLayer;
using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Customer;
using DataAccessLayer.Export;
using DataAccessLayer.Invoice;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;

namespace BusinessLayer
{
    public class BusinessManager
    {
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Client> ExportClients { get; set; } = new ObservableCollection<Client>();
        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>();
        public ObservableCollection<ArticleGroup> ArticleGroups { get; set; } = new ObservableCollection<ArticleGroup>();
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<OrderPosition> OrderPositions { get; set; } = new ObservableCollection<OrderPosition>();
        public ObservableCollection<InvoiceReport> InvoiceReports { get; set; } = new ObservableCollection<InvoiceReport>();

        public ObservableCollection<YearEndData> YearEndDataCollection { get; set; } = new ObservableCollection<YearEndData>();

        public ICustomerRepository CustomerRepository { get; }

        public IExportClientRepository ExportRepository { get; }
        public IArticleRepository ArticleRepository { get; }
        public IArticleGroupRepository ArticleGroupRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderPositionRepository OrderPositionRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }

        public BusinessManager(ICustomerRepository customerRepository, IExportClientRepository exportRepository, IArticleRepository articleRepository,
            IArticleGroupRepository articleGroupRepository, IOrderRepository orderRepository,
            IOrderPositionRepository orderPositionRepository, IInvoiceRepository invoiceRepository)
        {
            CustomerRepository = customerRepository;
            ExportRepository = exportRepository;
            ArticleRepository = articleRepository;
            ArticleGroupRepository = articleGroupRepository;
            OrderRepository = orderRepository;
            OrderPositionRepository = orderPositionRepository;
            InvoiceRepository = invoiceRepository;
        }

        public void MigrateDatabase()
        {
            new DataAccessManager().MigrateDatabase();
        }

        //Generic
        public void ModifySelected<T>(ObservableCollection<T> itemList, int index) where T : BusinessModelBase
        {
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                itemList[index].ReadOnly = false;
            }
        }
        public int GetIndexOfModifiableDataGridChild<T>(ObservableCollection<T> itemList) where T : BusinessModelBase
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].ReadOnly == false)
                {
                    return i;
                }
            }
            return -1;
        }

        //Customer
        public void LoadAllCustomersFromDb()
        {
            var customerDTOs = CustomerRepository.GetAll();

            Customers.Clear();

            foreach (var customerDTO in customerDTOs)
            {
                Customer customer = new Customer(customerDTO);
                Customers.Add(customer);
            }
        }

        public void CreateLocalCustomer()
        {
            Customers.Add(new Customer() { ReadOnly = false });
        }

        public void CancelModificationCustomer(ObservableCollection<Customer> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //Item not known by database yet
                    itemList.RemoveAt(index);
                }
                else
                {
                    //Item known by database
                    itemList[index] = new Customer(CustomerRepository.GetSingle(itemList[index].Id));
                }
            }
        }


        public void SaveModifiedCustomer(ObservableCollection<Customer> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);

            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                String validMassage = IsValidCustomer(itemList[index]);
                if (validMassage == null)
                {
                    if (itemList[index].Id == 0)
                    {

                        //not known by database yet
                        CustomerRepository.Add(itemList[index].ToCustomerDto());
                    }
                    else
                    {
                        //known by database
                        CustomerRepository.Update(itemList[index].ToCustomerDto());
                    }
                }
                else
                {
                    throw new Exception("Not Valid \r\n " + validMassage);
                }

                LoadAllCustomersFromDb();
            }
        }

        public void DeleteSelectedCustomer(int index)
        {
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                CustomerRepository.Delete(Customers[index].ToCustomerDto());
                LoadAllCustomersFromDb();
            }
        }

        public void FilterCustomers(string searchText)
        {
            //Refresh list prior to filtering
            LoadAllCustomersFromDb();

            for (int i = Customers.Count() - 1; i >= 0; i--)
            {
                var customer = Customers[i];

                if (customer.Id.ToString().Contains(searchText)
                    || customer.Clientnr != null && customer.Clientnr.Contains(searchText)
                    || customer.FirstName != null && customer.FirstName.Contains(searchText)
                    || customer.LastName != null && customer.LastName.Contains(searchText)
                    || customer.EMail != null && customer.EMail.Contains(searchText)
                    || customer.Website != null && customer.Website.Contains(searchText)
                    || customer.Address.Street != null && customer.Address.Street.Contains(searchText)
                    || customer.Address.StreetNo != null && customer.Address.StreetNo.Contains(searchText)
                    || customer.Address.City != null && customer.Address.City.Contains(searchText)
                    || customer.Address.Plz != null && customer.Address.Plz.Contains(searchText)
                    )
                {
                    //if any of the field contains the searchText, keep item in the list
                }
                else
                {
                    //none of the fields had the searchText -> remove from list
                    Customers.RemoveAt(i);
                }
            }

        }

        //ArticleGroup
        public void LoadAllArticleGroupsFromDb()
        {
            var articleGroupDTOs = ArticleGroupRepository.GetAll();

            ArticleGroups.Clear();

            foreach (var articleGroupDto in articleGroupDTOs)
            {
                var articleGroup = new ArticleGroup(articleGroupDto);
                ArticleGroups.Add(articleGroup);
            }
        }

        public void CreateLocalArticleGroup()
        {
            ArticleGroups.Add(new ArticleGroup() { ReadOnly = false });
        }

        public void CancelModificationArticleGroup(ObservableCollection<ArticleGroup> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //Item not known by database yet
                    itemList.RemoveAt(index);
                }
                else
                {
                    //Item known by database
                    itemList[index] = new ArticleGroup(ArticleGroupRepository.GetSingle(itemList[index].Id));
                }
            }
        }

        public void SaveModifiedArticleGroup(ObservableCollection<ArticleGroup> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);

            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //not known by database yet
                    ArticleGroupRepository.Add(itemList[index].ToArticleGroupDto());
                }
                else
                {
                    //known by database
                    ArticleGroupRepository.Add(itemList[index].ToArticleGroupDto());
                }

                LoadAllArticleGroupsFromDb();
            }
        }

        public void DeleteSelectedArticleGroup(int index)
        {
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {

                ArticleGroupRepository.Delete(ArticleGroups[index].ToArticleGroupDto());
                LoadAllArticleGroupsFromDb();
            }
        }

        //Article
        public void LoadAllArticlesFromDb()
        {
            var articleDTOs = ArticleRepository.GetAll();

            Articles.Clear();

            foreach (var articleDto in articleDTOs)
            {
                var article = new Article(articleDto);
                Articles.Add(article);
            }
        }

        public void CreateLocalArticle()
        {
            Articles.Add(new Article() { ReadOnly = false });
        }

        public void CancelModificationArticle(ObservableCollection<Article> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //Item not known by database yet
                    itemList.RemoveAt(index);
                }
                else
                {
                    //Item known by database
                    itemList[index] = new Article(ArticleRepository.GetSingle(itemList[index].Id));
                }
            }
        }

        public void SaveModifiedArticle(ObservableCollection<Article> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);

            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //not known by database yet
                    var articleDto = itemList[index].ToArticleDto();
                    articleDto.ArticleGroup = null;
                    ArticleRepository.Add(articleDto);
                }
                else
                {
                    //known by database
                    ArticleRepository.Update(itemList[index].ToArticleDto());
                }

                LoadAllArticlesFromDb();
            }
        }

        public void DeleteSelectedArticle(int index)
        {
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                ArticleRepository.Delete(Articles[index].ToArticleDto());
                LoadAllArticlesFromDb();
            }
        }

        public void FilterArticle(string searchText)
        {
            //Refresh list prior to filtering
            LoadAllArticlesFromDb();

            for (int i = Articles.Count() - 1; i >= 0; i--)
            {
                var article = Articles[i];

                if (article.Id.ToString().Contains(searchText)
                    || article.Price != null && article.Price.ToString().Contains(searchText)
                    || article.Name != null && article.Name.Contains(searchText)
                   )
                {
                    //if any of the field contains the searchText, keep item in the list
                }
                else
                {
                    //none of the fields had the searchText -> remove from list
                    Articles.RemoveAt(i);
                }
            }
        }

        //Order
        public void LoadAllOrdersFromDb()
        {
            var orderDTOs = OrderRepository.GetAll();

            Orders.Clear();

            foreach (var orderDto in orderDTOs)
            {
                var order = new Order(orderDto);
                Orders.Add(order);
            }
        }

        public void CreateLocalOrder()
        {
            Orders.Add(new Order() { ReadOnly = false });
        }

        public void CreateLocalOrderPos()
        {
            OrderPositions.Add(new OrderPosition() { ReadOnly = false });
        }

        public void CancelModificationOrder(ObservableCollection<Order> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //Item not known by database yet
                    itemList.RemoveAt(index);
                }
                else
                {
                    //Item known by database
                    itemList[index] = new Order(OrderRepository.GetSingle(itemList[index].Id));
                }
            }
        }

        public void SaveModifiedOrder(ObservableCollection<Order> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);

            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //not known by database yet
                    var newOrder = itemList[index].ToOrderDto();
                    newOrder.Customer = null;
                    OrderRepository.Add(newOrder);
                }
                else
                {
                    //known by database
                    OrderRepository.Update(itemList[index].ToOrderDto());
                }
                LoadAllOrdersFromDb();
            }
        }

        public void DeleteSelectedOrder(int index)
        {
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                OrderRepository.Delete(Orders[index].ToOrderDto());
                LoadAllOrdersFromDb();
            }
        }

        public void DeleteSelectedOrderPos(int index) {
            if (index == -1) {
                //Item not found
                return;
            }
            else {
                var orderIndex = OrderPositions[index].OrderId;

                OrderPositionRepository.Delete(OrderPositions[index].ToOrderPositionDto());
                LoadAllOrdersFromDb();
                LoadOrderPositionsForSpecificOrder(orderIndex);
            }
        }

        public void FilterOrder(string searchText)
        {
            //Refresh list prior to filtering
            LoadAllOrdersFromDb();

            for (int i = Orders.Count() - 1; i >= 0; i--)
            {
                var order = Orders[i];

                if (order.Id.ToString().Contains(searchText)
                    || order.Date != null && order.Date.ToString().Contains(searchText)
                    || order.Customer != null && order.Customer.ToString().Contains(searchText)
                   )
                {
                    //if any of the field contains the searchText, keep item in the list
                }
                else
                {
                    //none of the fields had the searchText -> remove from list
                    Orders.RemoveAt(i);
                }
            }
        }

        //OrderPosition
        public void LoadOrderPositionsForSpecificOrder(int orderId)
        {
            var orderPositionDTOs = OrderPositionRepository.GetAllByOrderId(orderId);

            OrderPositions.Clear();

            foreach (var orderPositionDto in orderPositionDTOs)
            {
                var orderPosition = new OrderPosition(orderPositionDto);
                OrderPositions.Add(orderPosition);
            }
        }

        public void CreateLocalOrderPosition()
        {
            OrderPositions.Add(new OrderPosition() { ReadOnly = false });
        }

        public void SaveModifiedOrderPos(ObservableCollection<OrderPosition> itemList, int selectedOrderId) {
            int index = GetIndexOfModifiableDataGridChild(itemList);

            if (index == -1) {
                //Item not found
                return;
            }
            else {
                if (itemList[index].Id == 0) {
                    //not known by database yet
                    var orderPosition = itemList[index].ToOrderPositionDto();
                    orderPosition.Article = null;
                    orderPosition.OrderId = selectedOrderId;
                    OrderPositionRepository.Add(orderPosition);
                }
                else {
                    //known by database
                    OrderPositionRepository.Update(itemList[index].ToOrderPositionDto());
                }

                LoadOrderPositionsForSpecificOrder(index);
            }
        }

        public void CancelModificationOrderPosition(ObservableCollection<OrderPosition> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //Item not known by database yet
                    itemList.RemoveAt(index);
                }
                else
                {
                    //Item known by database
                    itemList[index] = new OrderPosition(OrderPositionRepository.GetSingle(itemList[index].Id));
                }
            }
        }

        public void SaveModifiedOrderPosition(ObservableCollection<OrderPosition> itemList)
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);

            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if (itemList[index].Id == 0)
                {
                    //not known by database yet
                    OrderPositionRepository.Add(itemList[index].ToOrderPositionDto());
                }
                else
                {
                    //known by database
                    OrderPositionRepository.Update(itemList[index].ToOrderPositionDto());
                }

                LoadOrderPositionsForSpecificOrder(itemList[index].OrderId);
            }
        }

        public void DeleteSelectedOrderPosition(int index)
        {
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                int orderId = OrderPositions[index].OrderId;

                OrderPositionRepository.Delete(OrderPositions[index].ToOrderPositionDto());
                LoadOrderPositionsForSpecificOrder(orderId);
            }
        }

        public List<ArticleGroup> GetArticleGroupsRecursiveCte()
        {
            ArticleGroupDTO[] articleGroupDtos = ArticleGroupRepository.GetAllArticleGroupsRecursiveCte();
            var articleGroups = new List<ArticleGroup>();

            foreach (ArticleGroupDTO articleGroupDto in articleGroupDtos)
            {
                articleGroups.Add(new ArticleGroup(articleGroupDto));
            }
            return articleGroups;
        }

        public void LoadAllInvoicesFromDbbyDate(DateTime startDate, DateTime endDate)
        {
            var invoiceReportDTOs = InvoiceRepository.GetAllInvoicesbyDate(startDate, endDate);

            InvoiceReports.Clear();

            foreach (var invoicesReportDTO in invoiceReportDTOs)
            {
                InvoiceReport invoiceReport = new InvoiceReport(invoicesReportDTO);
                InvoiceReports.Add(invoiceReport);
            }

        }

        public void LoadAllYearEndData()
        {
            //Prepare YearEndDataCollection with all Categories
            YearEndDataCollection.Add(new YearEndData()
            {
                Category = "Total Orders"
            });
            YearEndDataCollection.Add(new YearEndData()
            {
                Category = "Average Article Quantity per Order"
            });
            YearEndDataCollection.Add(new YearEndData()
            {
                Category = "Average Sales per Customer"
            });
            YearEndDataCollection.Add(new YearEndData()
            {
                Category = "Total Sales"
            });
            YearEndDataCollection.Add(new YearEndData()
            {
                Category = "Total Articles in the System"
            });

            List<YearEndStatisticDTO> yearEndStatisticDTOs = InvoiceRepository.GetYearEndingData();

            //Convert YearEndStatisticDTO into YearEndData (basically pivot)
            foreach (var yearEndStatisticDTO in yearEndStatisticDTOs)
            {
                //Make sure all fields are either filled or at least a "0"
                if (yearEndStatisticDTO.TotalOrdersPerQuarter == null || yearEndStatisticDTO.TotalOrdersPerQuarter == String.Empty)
                {
                    YearEndDataCollection[0].QuarterData.Add("0");
                }
                else
                {
                    YearEndDataCollection[0].QuarterData.Add(yearEndStatisticDTO.TotalOrdersPerQuarter);
                }

                if (yearEndStatisticDTO.AvgArticleQtySumPerOrderPerQuarter == null || yearEndStatisticDTO.AvgArticleQtySumPerOrderPerQuarter == String.Empty)
                {
                    YearEndDataCollection[1].QuarterData.Add("0");
                }
                else
                {
                    YearEndDataCollection[1].QuarterData.Add(yearEndStatisticDTO.AvgArticleQtySumPerOrderPerQuarter);
                }

                if (yearEndStatisticDTO.AvgSumSalesPerCustomerPerQuarter == null || yearEndStatisticDTO.AvgSumSalesPerCustomerPerQuarter == String.Empty)
                {
                    YearEndDataCollection[2].QuarterData.Add("0");
                }
                else
                {
                    YearEndDataCollection[2].QuarterData.Add(yearEndStatisticDTO.AvgSumSalesPerCustomerPerQuarter);
                }

                if (yearEndStatisticDTO.SalesTotalPerQuarter == null || yearEndStatisticDTO.SalesTotalPerQuarter == String.Empty)
                {
                    YearEndDataCollection[3].QuarterData.Add("0");
                }
                else
                {
                    YearEndDataCollection[3].QuarterData.Add(yearEndStatisticDTO.SalesTotalPerQuarter);
                }

                if (yearEndStatisticDTO.TotalArticlesInTheSystem == null || yearEndStatisticDTO.TotalArticlesInTheSystem == String.Empty)
                {
                    YearEndDataCollection[4].QuarterData.Add("0");
                }
                else
                {
                    YearEndDataCollection[4].QuarterData.Add(yearEndStatisticDTO.TotalArticlesInTheSystem);
                }
            }
        }

        public string IsValidCustomer(Customer customer)
        {
            string validMessage = null;

            if (customer.Clientnr != null || customer.EMail != null || customer.Website != null || customer.Password != null || customer.Company != null) {
                if (customer.Clientnr != null)
                {
                    if (!IsValidCustomerNr(customer.Clientnr))
                    {
                        validMessage += "Clientnr ist nicht gültig!";
                    }
                }

                if (customer.EMail != null)
                {
                    if (!IsValidEmail(customer.EMail))
                    {
                        validMessage += "\r\n E-Mailadresse ist nicht gültig!";
                    }
                }

                if (customer.Website != null)
                {
                    if (!IsValidWebsite(customer.Website))
                    {
                        validMessage += "\r\n Webseite ist nicht gültig!";
                    }
                }

                if (customer.Password != null)
                {
                    if (!IsValidPassword(customer.Password))
                    {
                        validMessage += "\r\n Passwort ist nicht gültig!";
                    }
                }
            }
            else
            {
                validMessage += "\r\n Bitte füll alle Felder aus!";
            }

            return validMessage;
        }

        public string IsValidImport(Client customer)
        {

            string validMessage = null;

            if (!IsValidCustomerNr(customer.customerNr))
            {
                validMessage += "Clientnr ist nicht gültig!";
            }

            if (!IsValidEmail(customer.email))
            {
                validMessage += "\r\n E-Mailadresse ist nicht gültig!";
            }

            if (!IsValidWebsite(customer.website))
            {
                validMessage += "\r\n Webseite ist nicht gültig!";
            }
            if (!IsValidPassword(customer.password))
            {
                validMessage += "\r\n Passwort ist nicht gültig!";
            }

            return validMessage;
        }

        public bool IsValidCustomerNr(string customerNr)
        {
            string cliennrPattern = @"^CU\d{5}$";
            if (Regex.IsMatch(customerNr, cliennrPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^((?:[A-Za-z0-9!#$%&'*+\-\/=?^_`{|}~]|(?<=^|\.)'|'(?=$|\.|@)|(?<='.*)[ .](?=.*')|(?<!\.)\.){1,64})(@)((?:[A-Za-z0-9.\-])*(?:[A-Za-z0-9])\.(?:[A-Za-z0-9]){2,})$";
            if (Regex.IsMatch(email, emailPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidWebsite(string website)
        {
            string urlPattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            if (Regex.IsMatch(website, urlPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!”#$%&'()*+,-./:;<=>?]).{8,12}$";
            if (Regex.IsMatch(password, passwordPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void SerialzationJSON(DateTime startDate)
        {
            string fileName = @"c:/temp/Customer.json";

            using FileStream createStream = File.Create(fileName);
            var exportRepositoryDTO = ExportRepository.GetAllCustomersByValidDate(startDate);

            ExportClients.Clear();

            foreach (var exportDTO in exportRepositoryDTO)
            {
                var client = new Client(exportDTO);
                ExportClients.Add(client);
            }

            await JsonSerializer.SerializeAsync(createStream, ExportClients);
            await createStream.DisposeAsync();
        }

        public void DeserialzationJSON()
        {
            string fileName = @"c:/temp/ImportCustomer.json";
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                ObservableCollection<Client> importJsonCustomers = JsonSerializer.Deserialize<ObservableCollection<Client>>(jsonString)!;

                foreach (var importCustomer in importJsonCustomers)
                {
                    String validMassage = IsValidImport(importCustomer);
                    if (validMassage == null)
                    {
                        CustomerDTO customer = CustomerRepository.GetByCustomerNr(importCustomer.customerNr);
                        if (customer == null)
                        {
                            //not known by database yet
                            CustomerRepository.Add(importCustomer.ClienttoCustomer(customer));
                        }
                        else
                        {
                            //known by database
                            CustomerRepository.Update(importCustomer.ClienttoCustomer(customer));
                        }
                    }
                    else
                    {
                        throw new Exception("Not Valid \r\n " + validMassage);
                    }

                    LoadAllCustomersFromDb();
                }
            } else
            {
                throw new Exception(fileName + " existiert nicht. Bitte erstellen!");
            }
        }

        public void SerialzationXML(DateTime startDate)
        {
            string fileName = @"c:/temp/Customer.xml";

            var serializer = new XmlSerializer(typeof(Export));
            var writer = new Utf8StringWriter();

            var exportRepositoryDTO = ExportRepository.GetAllCustomersByValidDate(startDate);

            ExportClients.Clear();

            foreach (var exportDTO in exportRepositoryDTO)
            {
                var client = new Client(exportDTO);
                ExportClients.Add(client);
            }

            Export xmlExport = new Export(ExportClients);

            serializer.Serialize(writer, xmlExport);
            var serializedXml = writer.ToString();
            File.WriteAllText(fileName, serializedXml);
        }

        public void DeserialzationXML()
        {
            string fileName = @"c:/temp/ImportCustomer.xml";
            if (File.Exists(fileName))
            {
                var mySerializer = new XmlSerializer(typeof(Export));
                using var myFileStream = new FileStream(fileName, FileMode.Open);
                //var importXML = (Export)mySerializer.Deserialize(myFileStream);

                XDocument importXML = XDocument.Load(myFileStream);

                ExportClients.Clear();

                ExportClients = ClientXmlToCustomer(importXML);
                // Alternative XDocument
                // https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument?view=net-6.0
                foreach (var importCustomer in ExportClients)
                {
                    String validMassage = IsValidImport(importCustomer);
                    if (validMassage == null)
                    {
                        var customer = CustomerRepository.GetByCustomerNr(importCustomer.customerNr);
                        if (customer == null)
                        {
                            //not known by database yet
                            CustomerRepository.Add(importCustomer.ClienttoCustomer(customer));
                        }
                        else
                        {
                            //TODO
                            //known by database
                            CustomerRepository.Update(importCustomer.ClienttoCustomer(customer));
                        }
                    }
                    else
                    {
                        throw new Exception("Not Valid \r\n " + validMassage);
                    }

                    LoadAllCustomersFromDb();
                }
            } else { 
                throw new Exception(fileName + " existiert nicht. Bitte erstellen!"); 
            }
        }

        public ObservableCollection<Client> ClientXmlToCustomer(XDocument xmlDoc)
        {
            Debug.WriteLine("Start convertion");
            var customers = new ObservableCollection<Client>();
            foreach (var client in xmlDoc.Descendants("Kunde"))
            {
                Debug.WriteLine("Loop convertion");
                var customer = new Client();
                customer.customerNr = client.Attribute("CustomerNr").Value;
                customer.name = client.Element("Name").Value;
                customer.email = client.Element("EMail").Value;
                customer.website = client.Element("Website").Value;
                customer.password = client.Element("Password").Value;
                customer.company = client.Element("Company").Value;
                ClientAddress address = new ClientAddress();
                Debug.WriteLine(client.Element("Address").Element("Street").Value);
                address.street = client.Element("Address").Element("Street").Value;
                address.postalCode = client.Element("Address").Element("PostalCode").Value;
                //address.city = client.Element("Address").Element("City").Value;
                customer.address = address;
                customers.Add(customer);
            };
            return customers;
        }
    }
}

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding { get { return Encoding.UTF8; } }
}