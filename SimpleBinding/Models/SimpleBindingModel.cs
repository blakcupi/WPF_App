using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBinding.Models
{
    public class SimpleBindingModel
    {
        public double Korean { get; set; }
        public double Mathmetics { get; set; }
        public double English { get; set; }

        public double Score { get; set; }
        public bool IsRoundOff { get; set; } = false;
    }

    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
