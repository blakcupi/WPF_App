using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixer.Models
{
    public class ColorMixerModel
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public int MixedColor { get; set; }
    }

    public class  Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
