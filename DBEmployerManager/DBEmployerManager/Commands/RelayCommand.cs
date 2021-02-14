using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBEmployerManager.Commands
{
    class RelayCommand : ICommand
    {
        private Action<object> action;
        private Func<bool> function;

        public RelayCommand(Action<object> act, Func<bool> func)
        {
            action = act;
            function = func;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return function();
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
