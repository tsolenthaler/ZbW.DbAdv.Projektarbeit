using Autofac;
using DataAccessLayer.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Customer;
using DataAccessLayer.Invoice;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;
using DataAccessLayer.Export;

namespace BusinessLayer
{
    public class IoCSetup
    {
        public IContainer SetupContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ArticleRepository>().As<IArticleRepository>();
            containerBuilder.RegisterType<ArticleGroupRepository>().As<IArticleGroupRepository>();
            containerBuilder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            containerBuilder.RegisterType<ExportClientRepository>().As<IExportClientRepository>();
            containerBuilder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>();
            containerBuilder.RegisterType<OrderRepository>().As<IOrderRepository>();
            containerBuilder.RegisterType<OrderPositionRepository>().As<IOrderPositionRepository>();
            containerBuilder.RegisterType<BusinessManager>().As<BusinessManager>();
            var container = containerBuilder.Build();
            return container;
        }
    }
}
