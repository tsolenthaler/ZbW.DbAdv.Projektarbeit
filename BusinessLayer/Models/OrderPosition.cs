using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class OrderPosition : BusinessModelBase
    {

        private Article article;
        public Article Article
        {
            get => article;
            set => Set(ref article, value);
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        private Order order;
        public Order Order
        {
            get => order;
            set => Set(ref order, value);
        }

        public OrderPosition()
        {

        }

        public OrderPosition(OrderPositionDTO orderPositionDto)
        {
            Id = orderPositionDto.Id;
            Article = new Article(orderPositionDto.Article);
            Quantity = orderPositionDto.Quantity;
            Order = new Order(orderPositionDto.Order);
        }

    }
}