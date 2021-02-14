using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class EmployerModel : INotifyPropertyChanged
    {
        private int id;
        private string surname;
        private string name;
        private string patronymic;
        private DateTime born;
        private Gender employerGender;
        private UnitModel unit;

        public int Id { get { return id; } set { id = value; } }
        public string Surname { get { return surname; } set { surname = value; OnPropertyChanged("Surname"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string Patronymic { get { return patronymic; } set { patronymic = value; OnPropertyChanged("Patronymic"); } }
        public DateTime Born { get { return born; } set { born = value; OnPropertyChanged("Born"); } }
        public Gender EmployerGender { get { return employerGender; } set { employerGender = value; OnPropertyChanged("EmployerGender"); } }
        public UnitModel Unit { get { return unit; } set { unit = value; OnPropertyChanged("Unit"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string pn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pn));
            }
        }

        public override string ToString()
        {
            return Surname + " " + Name + " " + Patronymic;
        }
    }
}
