using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKiosk.Models
{
    public class BurgerType
    {
        public required string Name { get; set; }
        public decimal Amount { get; set; }
    }

    public class CoffeeType : Notifier
    {
        public required string Name { get; set; }
        public decimal Amount { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }

    public class SimpleKioskModel
    {
        public decimal BurgetAmount { get; set; }

        public decimal CoffeeAmount { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
