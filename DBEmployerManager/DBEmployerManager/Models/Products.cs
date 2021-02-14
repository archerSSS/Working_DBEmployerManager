using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class Products
    {
        private int productId;
        private int ordersId;
        private string productName;
        private int count;
        private decimal price;
        public virtual int ProductId { get { return productId; } set { productId = value; } }
        public virtual int OrdersId { get { return ordersId; } set { ordersId = value; } }
        public virtual string ProductName { get { return productName; } set { productName = value; } }
        public virtual int Count { get { return count; } set { count = value; } }
        public virtual decimal Price { get { return price; } set { price = value; } }
    }
}
