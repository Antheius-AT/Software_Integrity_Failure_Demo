using System;
using System.Windows.Input;

namespace Demo_Client_Windows
{
    public class Command : ICommand
    {
        private Action<object> action;
        private Func<object, bool> canExecute;

        public Command(Action<object> action, Func<object, bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            if (this.canExecute(parameter))
            {
                this.action(parameter);
            }
        }
    }
}