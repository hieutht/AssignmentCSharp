using System;
using System.Collections.Generic;

namespace DatabaseHandler.Entity
{
    public class Order
    {
        public string Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public double TotalMoney { get; set; }
        public int Status { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}