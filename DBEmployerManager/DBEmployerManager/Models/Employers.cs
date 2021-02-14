using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class Employers
    {
        private int id;
        private string firstName;
        private string lastName;
        private string patronymic;
        private string born;
        private string gender;
        private string unitName;

        public virtual int Id { get { return id; } set { id = value; } }
        public virtual string FirstName { get { return firstName; } set { firstName = value; } }
        public virtual string LastName { get { return lastName; } set { lastName = value; } }
        public virtual string Patronymic { get { return patronymic; } set { patronymic = value; } }
        public virtual string Born { get { return born; } set { born = value; } }
        public virtual string Gender { get { return gender; } set { gender = value; } }
        public virtual string UnitName { get { return unitName; } set { unitName = value; } }
    }
}
