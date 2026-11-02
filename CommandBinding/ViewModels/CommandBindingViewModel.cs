using CommandBinding.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandBinding.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T field, T value, string propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }

    public class CommandBindingViewModel : ViewModelBase
    {
        private string _message = "Request Button Click";
        public string Message
        {
            get => _message;
            set => SetProperty<string>(ref _message, value, nameof(Message));
        }

        public ICommand MessageButtonCommand { get; }

        public CommandBindingViewModel()
        {
            MessageButtonCommand = new RelayCommand((param) => GreetingMessage());
        }

        private void GreetingMessage() => Message = "Welcome to Command Binding!!";
    }
}
