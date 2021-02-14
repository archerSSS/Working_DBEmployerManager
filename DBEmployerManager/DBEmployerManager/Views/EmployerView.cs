using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Views
{
    class EmployerView
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Born { get; set; }
        public string Gender { get; set; }
        public object Unit { get; set; }
    }
}
