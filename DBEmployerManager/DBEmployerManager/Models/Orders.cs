using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class Orders
    {
        private int orderId;
        private string contractor;
        private string date;
        private int employers_Id;
        public virtual int OrderId { get { return orderId; } set { orderId = value; } }
        public virtual string Contractor { get { return contractor; } set { contractor = value; } }
        public virtual string Date { get { return date; } set { date = value; } }
        public virtual int Employers_Id { get { return employers_Id; } set { employers_Id = value; } }

    }
}
