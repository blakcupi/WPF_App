using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataGridStyle.Models;
using DataGridStyle.Command;
using System.Collections.ObjectModel;

namespace DataGridStyle.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public DataGridViewModel()
        {

        }

        public ObservableCollection<DepartmentModel> Departments { get; } =
        [
            new("HR", "Human Resources"),
            new("IT", "Information Technology"),
            new("SALES", "Sales"),
            new("MKT", "Marketing"),
            new("OPS", "Operations")
        ];

        private DepartmentModel? _selectedDepartment;
        public DepartmentModel? SelectedDepartment
        {
            get => _selectedDepartment;
            set => SetProperty<DepartmentModel?>(ref _selectedDepartment, value);
        }

        public ObservableCollection<PersonModel> People { get; set; } =
            [
                new() { ID = 1, Name = "Alice", Age = 30, IsActive = true, Department = new("HR", "Human Resources") },
                new() { ID = 2, Name = "Bob", Age = 25, IsActive = false, Department = new("IT", "Information Technology") },
                new() { ID = 3, Name = "Charlie", Age = 35, IsActive = true, Department = new("SALES", "Sales") },
                new() { ID = 4, Name = "Diana", Age = 28, IsActive = true, Department = new("MKT", "Marketing") },
                new() { ID = 5, Name = "Ethan", Age = 40, IsActive = false, Department = new("OPS", "Operations") },
                new() { ID = 6, Name = "Fiona", Age = 32, IsActive = true, Department = new("SALES", "Sales") },
                new() { ID = 7, Name = "George", Age = 29, IsActive = false, Department = new("MKT", "Marketing") },
                new() { ID = 8, Name = "Hannah", Age = 27, IsActive = true, Department = new("SALES", "Sales") },
            ];

        private PersonModel? _selectedPerson;
        public PersonModel? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                SetProperty<PersonModel?>(ref _selectedPerson, value);
            }
        }
    }
}
