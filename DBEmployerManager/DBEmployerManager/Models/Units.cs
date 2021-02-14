using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class Units
    {
        private int id;
        private string unitName;
        private int headId;
        public virtual int Id { get { return id; } set { id = value; } }
        public virtual string UnitName { get { return unitName; } set { unitName = value; } }
        public virtual int HeadId { get { return headId; } set { headId = value; } }
    }
}
