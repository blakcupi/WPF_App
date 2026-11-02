using SimpleBinding.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBinding.ViewModels
{
    public class SimpleBindingViewModel : Notifier
    {
        private SimpleBindingModel _model;
        public SimpleBindingViewModel()
        {
            _model = new SimpleBindingModel();
        }

        public ObservableCollection<string> CalcTypes { get; } = ["SUM", "MAX", "AVERAGE"];

        private string? _selectedCalcType;
        public string? SelectedCalcType
        {
            get
            {
                if (_selectedCalcType == null)
                    _selectedCalcType = CalcTypes.FirstOrDefault();

                return _selectedCalcType;
            }
            set
            {
                _selectedCalcType = value;
                OnPropertyChanged(nameof(SelectedCalcType));
            }
        }

        private string _koreanText = "0";
        public string KoreanText
        {
            get { return _koreanText; }
            set
            {
                if (_koreanText == value)
                    return;

                _koreanText = value;
                OnPropertyChanged(nameof(KoreanText));

                Korean = ConvertType(_koreanText);
            }
        }

        private string _mathmeticsText = "0";
        public string MathmeticsText
        {
            get { return _mathmeticsText; }
            set
            {
                if (_mathmeticsText == value)
                    return;

                _mathmeticsText = value;
                OnPropertyChanged(nameof(MathmeticsText));

                Mathmetics = ConvertType(_mathmeticsText);
            }
        }

        private string _englishText = "0";
        public string EnglishText
        {
            get { return _englishText; }
            set
            {
                if (_englishText == value)
                    return;

                _englishText = value;
                OnPropertyChanged(nameof(EnglishText));

                English = ConvertType(_englishText);
            }
        }

        private static double ConvertType(string? text) => double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out var v) ? v : 0;

        public double Korean
        {
            get => _model.Korean;
            set
            {
                if (_model.Korean == value) return;

                _model.Korean = value;
                OnPropertyChanged(nameof(Korean));
            }
        }

        public double Mathmetics
        {
            get => _model.Mathmetics;
            set
            {
                if (_model.Mathmetics == value) return;

                _model.Mathmetics = value;
                OnPropertyChanged(nameof(Mathmetics));
            }
        }

        public double English
        {
            get => _model.English;
            set
            {
                if (_model.English == value) return;

                _model.English = value;
                OnPropertyChanged(nameof(English));
            }
        }

        public double Score
        {
            get => _model.Score;
            set
            {
                _model.Score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool IsRoundOff
        {
            get => _model.IsRoundOff;
            set
            {
                _model.IsRoundOff = value;
                OnPropertyChanged(nameof(IsRoundOff));
            }
        }

        private void UpdateCalculation()
        {
            double score = 0;
            switch (SelectedCalcType)
            {
                case "SUM":
                    score = Korean + Mathmetics + English;
                    break;
                case "MAX":
                    score = new double[] { Korean, Mathmetics, English }.Max();
                    break;
                case "AVERAGE":
                    score = (Korean + Mathmetics + English) / 3.0;
                    break;
                default:
                    break;
            }

            Score = IsRoundOff ? Math.Round(score) : score;
        }

        public void CalculationButton_Click(object sender, EventArgs e)
        {
            UpdateCalculation();
        }
    }
}
