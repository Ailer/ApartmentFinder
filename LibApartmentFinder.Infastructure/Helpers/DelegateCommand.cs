using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibApartmentFinder.Infastructure.Helpers
{
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            if (this._canExecute != null)
            {
                return this._canExecute((T)parameter);
            }

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this._execute((T)parameter);
        }

        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
    }

    public class DelegateCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            if (this._canExecute != null)
            {
                return this._canExecute();
            }

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this._execute();
        }

        public DelegateCommand(Action execute)
            : this(execute, null)
        { }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
    }
}
