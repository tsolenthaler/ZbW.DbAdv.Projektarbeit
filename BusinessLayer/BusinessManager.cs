using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class BusinessManager
    {
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>();
        public ObservableCollection<ArticleGroup> ArticleGroups { get; set; } = new ObservableCollection<ArticleGroup>();
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<OrderPosition> OrderPositions { get; set; } = new ObservableCollection<OrderPosition>();
        public ObservableCollection<Invoice> Invoices { get; set; } = new ObservableCollection<Invoice>();


        public DataAccessManager DataAccessManager { get; set; } = new DataAccessManager();

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
            var customerDTOs = DataAccessManager.GetAllCustomers();

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
                    itemList[index] = new Customer(DataAccessManager.GetCustomerById(itemList[index].Id));
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
                if (itemList[index].Id == 0)
                {
                    //not known by database yet
                    DataAccessManager.NewCustomer(itemList[index].ToCustomerDto());               
                }
                else
                {
                    //known by database
                    DataAccessManager.UpdateCustomer(itemList[index].ToCustomerDto());
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
                DataAccessManager.DeleteCustomerById(Customers[index].Id);
                LoadAllCustomersFromDb();
            }
        }

        public void FilterCustomers(string searchText)
        {
            //Refresh list prior to filtering
            LoadAllCustomersFromDb();

            for(int i= Customers.Count()-1; i>=0; i--)
            {
                var customer = Customers[i];

                if (customer.Id.ToString().Contains(searchText)
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
            var articleGroupDTOs = DataAccessManager.GetAllArticleGroup();

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
                    itemList[index] = new ArticleGroup(DataAccessManager.GetArticleGroupById(itemList[index].Id));
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
                    DataAccessManager.NewArticleGroup(itemList[index].ToArticleGroupDto());
                }
                else
                {
                    //known by database
                    DataAccessManager.UpdateArticleGroup(itemList[index].ToArticleGroupDto());
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
                DataAccessManager.DeleteArticleGroup(ArticleGroups[index].Id);
                LoadAllArticleGroupsFromDb();
            }
        }

        //Article
        public void LoadAllArticlesFromDb()
        {
            var articleDTOs = DataAccessManager.GetAllArticle();

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
                    itemList[index] = new Article(DataAccessManager.GetArticleById(itemList[index].Id));
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
                    DataAccessManager.NewArticle(itemList[index].ToArticleDto());
                }
                else
                {
                    //known by database
                    DataAccessManager.UpdateArticle(itemList[index].ToArticleDto());
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
                DataAccessManager.DeleteArticleById(Articles[index].Id);
                LoadAllArticlesFromDb();
            }
        }

        public void FilterArticle(string searchText)
        {
            //Refresh list prior to filtering
            LoadAllArticlesFromDb();

            for (int i = Articles.Count()-1; i>=0; i--)
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
            var orderDTOs = DataAccessManager.GetAllOrders();

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
                    itemList[index] = new Order(DataAccessManager.GetOrderByID(itemList[index].Id));
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
                    DataAccessManager.NewOrder(itemList[index].ToOrderDto());
                }
                else
                {
                    //known by database
                    DataAccessManager.UpdateOrder(itemList[index].ToOrderDto());
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
                DataAccessManager.DeleteOrderById(Orders[index].Id);
                LoadAllOrdersFromDb();
            }
        }

        public void FilterOrder(string searchText)
        {
            //Refresh list prior to filtering
            LoadAllOrdersFromDb();

            for (int i = Orders.Count()-1; i>=0; i--)
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
                    Articles.RemoveAt(i);
                }
            }
        }

        //OrderPosition
        public void LoadOrderPositionsForSpecificOrder(int orderId)
        {
            var orderPositionDTOs = DataAccessManager.GetOrderPositionByOrderID(orderId);

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
                    itemList[index] = new OrderPosition(DataAccessManager.GetOrderPositionByID(itemList[index].Id));                    
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
                    DataAccessManager.NewOrderPosition(itemList[index].ToOrderPositionDto());
                }
                else
                {
                    //known by database
                    DataAccessManager.UpdateOrderPosition(itemList[index].ToOrderPositionDto());
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

                DataAccessManager.DeleteOrderPositionById(OrderPositions[index].Id);
                LoadOrderPositionsForSpecificOrder(orderId);
            }
        }

        public void LoadAllInvoicesFromDb()
        {
            var invoicesDTOs = DataAccessManager.GetAllInvoices();

            Invoices.Clear();

            foreach (var invoicesDTO in invoicesDTOs)
            {
                Invoice invoice = new Invoice(invoicesDTO);
                Invoices.Add(invoice);
            }
        }


    }
}
