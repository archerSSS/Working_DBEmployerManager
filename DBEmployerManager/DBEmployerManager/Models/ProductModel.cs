using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class ProductModel : INotifyPropertyChanged
    {
        private int id;
        private OrderModel order;
        private string name;
        private int count;
        private decimal price;
        public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public OrderModel Order { get { return order; } set { order = value; OnPropertyChanged("Order"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public int Count { get { return count; } set { count = value; OnPropertyChanged("Count"); } }
        public decimal Price { get { return price; } set { price = value; OnPropertyChanged("Price"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string pn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pn));
            }
        }
    }
}
