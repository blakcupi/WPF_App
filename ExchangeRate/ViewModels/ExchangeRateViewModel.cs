using ExchangeRate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExchangeRate.Models.ExchangeRateModel;

namespace ExchangeRate.ViewModels
{
    public class ExchangeRateViewModel : Notifier
    {
        public ObservableCollection<ExchangeNationType> ExchangeRates { get; } = new ObservableCollection<ExchangeNationType>
        {
            new() { NationCode = "USD", Rate = 1.3270m },
            new() { NationCode = "AUD", Rate = 0.88989m },
        };

        private ExchangeNationType _selectedExchangeRate;

        public ExchangeNationType SelectedExchangeRate
        {
            get => _selectedExchangeRate;
            set
            {
                if (ReferenceEquals(_selectedExchangeRate, value))
                    return;

                _selectedExchangeRate = value;
                OnPropertyChanged(nameof(SelectedExchangeRate));

                // 선택 변경에 따른 화면 상태 갱신
                OnExchangeRateChanged();
            }
        }

        private void OnExchangeRateChanged()
        {
            if (SelectedExchangeRate == null)
                return;

            decimal convertedAmount = ConvertCurrency(CurrentCurrency);
            KRW = Math.Round(convertedAmount, 0);

            System.Diagnostics.Debug.WriteLine($"선택 환율: {SelectedExchangeRate.NationCode}");
        }

        public ExchangeRateModel model;

        public ExchangeRateViewModel()
        {
            model = new ExchangeRateModel();

            _selectedExchangeRate = ExchangeRates.FirstOrDefault() ?? new ExchangeNationType() { NationCode = "USD", Rate = 1.3270m };
        }

        public decimal KRW
        {
            get { return model.KRW; }
            set { 
                model.KRW = value;
                OnPropertyChanged(nameof(KRW));
            }
        }

        public decimal CurrentCurrency
        {
            get { return model.CurrentCurrency; }
            set
            {
                model.CurrentCurrency = value;
                OnPropertyChanged(nameof(CurrentCurrency));
            }
        }


        public decimal ConvertCurrency(decimal amount)
        {
            decimal fromRate = SelectedExchangeRate.Rate;
            decimal convertedAmount = amount * 1000 * fromRate;

            return convertedAmount;
        }

        public void GetExchangeRate_Click(object sender, EventArgs e)
        {
            decimal convertedAmount = ConvertCurrency(CurrentCurrency);

            KRW = Math.Round(convertedAmount, 0);
        }
    }
}
