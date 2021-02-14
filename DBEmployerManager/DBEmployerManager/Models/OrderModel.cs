using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBEmployerManager.Models
{
    class OrderModel : INotifyPropertyChanged
    {
        private int number;
        private string contractor;
        private DateTime date;
        private EmployerModel employer;
        public int Number { get { return number; } set { number = value; OnPropertyChanged("Number"); } }
        public string Contractor { get { return contractor; } set { contractor = value; OnPropertyChanged("Contractor"); } }
        public DateTime Date { get { return date; } set { date = value; OnPropertyChanged("Date"); } }
        public EmployerModel Employer { get { return employer; } set { employer = value; OnPropertyChanged("Employer"); } }

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
            return "Order number: " + Number;
        }
    }
}
