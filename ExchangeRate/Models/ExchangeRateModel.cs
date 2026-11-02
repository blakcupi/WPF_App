using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class ExchangeRateModel
    {
        public class ExchangeNationType
        {
            public required string NationCode { get; set; }
            public decimal Rate { get; set; }
        }

        public decimal KRW { get; set; }
        public decimal CurrentCurrency { get; set; }

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
