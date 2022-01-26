using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace BusinessLayer
{
    public class BusinessManager
    {
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        private List<Article>? articleList;
        private List<ArticleGroup>? articelGroupList;
        private List<Order>? orderList;

        public BusinessManager()
        {
            GenerateSampleData();
        }
        private void GenerateSampleData()
        {
            Customer customer = new Customer();
            customer.Id = 1;
            customer.FirstName = "Hans";
            customer.LastName = "Muster";
            customer.Address = new Address();
            customer.Address.Street = "Musterstrasse";
            customer.ReadOnly = true;

            Customers.Add(customer);

            Customer customer2 = new Customer();
            customer2.Id = 2;
            customer2.FirstName = "Franz";
            customer2.LastName = "Gluggeri";
            customer2.Address = new Address();
            customer2.Address.Street = "Teststrasse";
            customer2.ReadOnly = true;

            Customers.Add(customer2);
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
                    //To be implemented
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




    }
}
