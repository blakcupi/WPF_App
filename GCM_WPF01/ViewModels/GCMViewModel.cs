using GCM.Models;

namespace GCM.ViewModels
{
    public class GCMViewModel : Notifier
    {
        public GCMModel model;

        public GCMViewModel() 
        {
            model = new GCMModel();
        }

        public int FirstNumber
        {
            get { return model.FirstNumber; }
            set { 
                model.FirstNumber = value;
                OnPropertyChanged(nameof(FirstNumber));
            }
        }

        public int SecondNumber
        {
            get { return model.SecondNumber; }
            set { 
                model.SecondNumber = value;
                OnPropertyChanged(nameof(SecondNumber));
            }
        }

        public int GCMResult
        {
            get { return model.GCMResult; }
            set { 
                model.GCMResult = value;
                OnPropertyChanged(nameof(GCMResult));
            }
        }

        private int CalculateGCM()
        {
            int num01 = Math.Abs(FirstNumber);
            int num02 = Math.Abs(SecondNumber);

            while (num02 != 0)
            {
                int remainder = num01 % num02;
                num01 = num02;
                num02 = remainder;
            }

            return num01;
        }

        public void CalculateGCM_Click()
        {
            int gcm = CalculateGCM();

            GCMResult = gcm;
        }
    }
}   