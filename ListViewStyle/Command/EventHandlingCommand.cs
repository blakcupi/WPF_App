using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListViewStyle.Command
{
    public class RelayCommand : ICommand
    {
        public Action<object?> _execute;
        public Predicate<object?>? _canExecute;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> exec, Predicate<object?>? canExec = null)
        {
            _execute = exec ?? throw new ArgumentNullException(nameof(exec));
            _canExecute = canExec;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute?.Invoke(parameter);

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
