using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using SimpleKiosk.Models;

namespace SimpleKiosk.ViewModels
{
    public class SimpleKioskViewModel : Notifier
    {
        private SimpleKioskModel _model;

        public ObservableCollection<BurgerType> Burgers { get; } =
        [
            new () {Name = "Cheese Burger", Amount = 4500 },
            new() {Name= "Chicken Burger", Amount = 5500 },
            new() {Name= "Shrimp Burger", Amount = 5000 },
        ];
        private BurgerType? _selectedBurgerType = null;

        public BurgerType? SelectedBurgerType
        {
            get => _selectedBurgerType;
            set
            {
                if (ReferenceEquals(_selectedBurgerType, value))
                    return;

                _selectedBurgerType = value;

                OnPropertyChanged(nameof(SelectedBurgerType));
                OnBurgerTypeChanged();// 선택 변경에 따른 화면 상태 갱신
            }
        }

        private void OnBurgerTypeChanged()
        {
            if (SelectedBurgerType == null)
                return;

            BurgerAmount = SelectedBurgerType.Amount;

            decimal convertedAmount = CalcAmount();
            TotalAmount = Math.Round(convertedAmount, 0);

            System.Diagnostics.Debug.WriteLine($"선택 버거: {SelectedBurgerType.Name}");
        }

        public ObservableCollection<CoffeeType> Coffees { get; } =
        [
            new () {Name = "Americano", Amount = 3000, IsSelected = false },
            new() {Name = "Cappuccino", Amount = 3500, IsSelected = false },
            new() {Name = "Latte", Amount = 4000, IsSelected = false },
            new() {Name = "Espresso", Amount = 25000, IsSelected = false },
        ];

        public SimpleKioskViewModel()
        {
            _model = new SimpleKioskModel();

            foreach (var coffee in Coffees)
                coffee.PropertyChanged += Coffee_PropertyChanged;
        }

        private void Coffee_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CoffeeType.IsSelected))
                return;

            decimal total = CalcAmount();
            TotalAmount = total;
        }

        private decimal BurgerAmount
        {
            get { return _model.BurgetAmount; }
            set
            {
                _model.BurgetAmount = value;
            }
        }

        private decimal CoffeeAmount
        {
            get { return _model.CoffeeAmount; }
            set
            {
                _model.CoffeeAmount = value;
            }
        }

        private void UpdateCoffeeAmount()
        {
            CoffeeAmount = Coffees
                .Where(coffee => coffee.IsSelected)
                .Sum(coffee => coffee.Amount);
        }

        public decimal TotalAmount
        {
            get { return _model.TotalAmount; }
            set
            {
                _model.TotalAmount = value;

                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        private decimal CalcAmount()
        {
            UpdateCoffeeAmount();

            decimal total = BurgerAmount + CoffeeAmount;

            return total;
        }
    }
}
