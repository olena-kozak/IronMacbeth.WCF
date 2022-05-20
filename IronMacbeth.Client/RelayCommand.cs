using System;
using System.Windows.Input;

namespace IronMacbeth.Client.ViewModel
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        public Func<object, bool> CanExecuteFunc = null;

        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunc == null)
            {
                return true;
            }
            return CanExecuteFunc(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public void Execute(object parameter)
        {
            _action(parameter);
        }

        #endregion
    }
}
