using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataGridStyle.Models
{
    public class PersonModel
    {
        public int ID { get; set; }

        public string  Name { get; set; } = "";

        public DepartmentModel Department { get; set; }

        public int Age { get; set; }
        
        public bool IsActive { get; set; }

        public PersonModel()
        {
            Department = new();
        }
    }

    public class DepartmentModel
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";

        public DepartmentModel()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public DepartmentModel(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
