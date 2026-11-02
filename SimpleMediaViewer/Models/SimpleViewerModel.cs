using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediaViewer.Models
{
    public class SimpleViewerModel
    {
        public string MediaName { get; set; }
        public string MediaPath { get; set; }
        public TimeOnly MediaTime { get; set; }
    }

    public class SimpleMediaInfo() : Notifier
    {
        public required string MediaName { get; set; } 
        public required string MediaPath { get; set; }

        public TimeOnly MediaTime { get; set; } 
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
