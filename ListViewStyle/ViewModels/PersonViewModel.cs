using ListViewStyle.Command;
using ListViewStyle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ListViewStyle.ViewModels
{
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

    public class PersonViewModel : ViewModelBase
    {
        private readonly PersonModel _model;

        public RelayCommand DeleteCommand { get; set; }

        public PersonViewModel()
        {
            _model = new PersonModel();

            DeleteCommand = new RelayCommand(p => Delete(p as PersonModel), CanDelete);
        }

        private void Delete(PersonModel? p)
        {
            if (p == null)
                return;

            People.Remove(p!);
        }

        private bool CanDelete(object? parameter)
        {
            return parameter is PersonModel person
                && ReferenceEquals(person, SelectedPerson)
                && People.Contains(person);
        }


        public ObservableCollection<PersonModel> People { get; } =
            [
            new() {Id = 1, Name = "KimCG", Age = 55, IsActive = true },
            new() {Id = 2, Name = "LeeJA", Age = 35, IsActive = true },
            new() {Id = 3, Name = "HongGD", Age = 21, IsActive = false },
            new() {Id = 4, Name = "ParkCH", Age = 61, IsActive = false },
            new() {Id = 5, Name = "AhMooKae", Age = 43, IsActive = true }
            ];

        private PersonModel? _selectedPerson;
        public PersonModel? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (SetProperty<PersonModel?>(ref _selectedPerson, value))
                    DeleteCommand.OnCanExecuteChanged();
            }
        }

        public string Name
        {
            get => _model.Name;
            set
            {
                if (_model.Name == null)
                    return;

                _model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get => _model.Age;
            set
            {
                if (_model.Name == null)
                    return;

                _model.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public bool IsActive
        {
            get => _model.IsActive;
            set
            {
                if (_model.IsActive != null)
                {
                    _model.IsActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

    }
}
