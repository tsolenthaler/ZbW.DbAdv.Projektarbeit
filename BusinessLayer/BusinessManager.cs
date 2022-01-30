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

        private List<Article>? articleList;
        private List<ArticleGroup>? articelGroupList;
        private List<Order>? orderList;

        public DataAccessManager DataAccessManager { get; set; } = new DataAccessManager();

        public void LoadAllCustomersFromDb()
        {
            var customerDTOs = DataAccessManager.GetAllCustomers();

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

        public void CancelModification<T>(ObservableCollection<T> itemList) where T : BusinessModelBase
        {
            int index = GetIndexOfModifiableDataGridChild(itemList);
            if (index == -1)
            {
                //Item not found
                return;
            }
            else
            {
                if(itemList[index].Id == 0)
                {
                    //Item not known by database yet
                    itemList.RemoveAt(index);
                }
                else
                {
                    //Item known by database
                    itemList[index].ReadOnly = true;

                    //implement reload customer from database
                }
            }
        }

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

        public void SaveModified<T>(ObservableCollection<T> itemList) where T : BusinessModelBase
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
                    //Implement saving onto database
                }
                else
                {
                    //Item known by database
                    itemList[index].ReadOnly = true;

                    //Implement saving onto database
                }
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

        public void DeleteSelected<T>(ObservableCollection<T> itemList, int index) where T : BusinessModelBase
        {
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
                    CancelModification(itemList);
                }
                else
                {
                    //Item known by database

                    //Implement deleting from database

                    itemList.RemoveAt(index);
                    
                }
            }
        }


    }
}
