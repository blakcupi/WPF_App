using ColorMixer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixer.ViewModels
{
    public class ColorMixerViewModel : Notifier
    {
        private ColorMixerModel _model;

        public ColorMixerViewModel()
        {
            _model = new ColorMixerModel();

            Red = 255;
            Green = 255;
            Blue = 255;
        }

        public int Red
        {
            get { return _model.Red; }
            set
            {
                if (_model.Red != value)
                {
                    _model.Red = value;

                    MixColors();
                    NotifyPropertyChanged(nameof(Red));
                }
            }
        }

        public int Green
        {
            get { return _model.Green; }
            set
            {
                if (_model.Green != value)
                {
                    _model.Green = value;

                    MixColors();
                    NotifyPropertyChanged(nameof(Green));
                }
            }
        }

        public int Blue
        {
            get { return _model.Blue; }
            set
            {
                if (_model.Blue != value)
                {
                    _model.Blue = value;

                    MixColors();
                    NotifyPropertyChanged(nameof(Blue));
                }
            }
        }

        public int MixedColor
        {
            get { return _model.MixedColor; }
            set
            {
                if (_model.MixedColor != value)
                {
                    _model.MixedColor = value;
                    NotifyPropertyChanged(nameof(MixedColor));
                    NotifyPropertyChanged(nameof(MixedColorHex));
                }
            }
        }

        public string MixedColorHex => $"#{MixedColor:X6}";

        private void MixColors() => MixedColor = (Red << 16) | (Green << 8) | Blue;        
    }
}
