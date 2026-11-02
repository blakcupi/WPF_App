using RandomGen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGen.ViewModels
{
    public class RandomGenViewModel : Notifier
    {
        private RandomGenModel _model;

        public RandomGenViewModel()
        {
            _model = new RandomGenModel();
        }

        public int StartNumber
        {
            get { return _model.StartNumber; }
            set
            {
                _model.StartNumber = value;
                OnPropertyChanged(nameof(StartNumber));
            }
        }

        public int EndNumber
        {
            get { return _model.EndNumber; }
            set
            {
                _model.EndNumber = value;
                OnPropertyChanged(nameof(EndNumber));
            }
        }

        public int RandomNumber
        {
            get { return _model.RandomNumber; }
            set
            {
                _model.RandomNumber = value;
                OnPropertyChanged(nameof(RandomNumber));
                OnPropertyChanged(nameof(FirstImage));
                OnPropertyChanged(nameof(SecondImage));
            }
        }

        private string _firstImage = "0";
        public string FirstImage
        {
            get {
                _firstImage = RandomNumber != 0 ? RandomNumber.ToString().Substring(0, 1) : "0";
                return $"../Images/{_firstImage}.png";
            }
            set
            {
                _firstImage = value;
            }
        }

        private string _secondImage = "0";
        public string SecondImage
        {
            get
            {
                _secondImage = RandomNumber != 0 && RandomNumber.ToString().Length > 1 ? RandomNumber.ToString().Substring(1, 1) : "0";
                return $"../Images/{_secondImage}.png";
            }
            set
            {
                _secondImage = value;
            }
        }

        private void GenerateRandomNumber()
        {
            Random random = new Random();
            RandomNumber = random.Next(StartNumber, EndNumber + 1);
        }

        public void GenerateButton_Click(object sender, EventArgs e)
        {
            GenerateRandomNumber();
        }
    }
}
