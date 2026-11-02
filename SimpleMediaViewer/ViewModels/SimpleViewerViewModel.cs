using Microsoft.Xaml.Behaviors.Core;
using SimpleMediaViewer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMediaViewer.ViewModels
{
    public class SimpleViewerViewModel : Notifier
    {
        public SimpleViewerModel _model;
        public SimpleViewerViewModel()
        {
            _model = new SimpleViewerModel();
        }

        public string MediaName
        {
            get { return _model.MediaName; }
            set
            {
                if (_model.MediaName != value)
                {
                    _model.MediaName = value;
                    OnPropertyChanged(nameof(MediaName));
                }
            }
        }

        public string MediaPath
        {
            get { return _model.MediaPath; }
            set
            {
                if (_model.MediaPath != value)
                {
                    _model.MediaPath = value;
                    OnPropertyChanged(nameof(MediaPath));
                }
            }
        }

        public TimeOnly MediaTime
        {
            get { return _model.MediaTime; }
            set
            {
                if (_model.MediaTime != value)
                {
                    _model.MediaTime = value;
                    OnPropertyChanged(nameof(MediaTime));
                }
            }
        }

        private static string VideoPath(string file) => System.IO.Path.Combine(AppContext.BaseDirectory, "Videos", file);

        public ObservableCollection<SimpleMediaInfo> MediaInfos { get; } =
        [
            new () { MediaName = "Sample Video 1", MediaPath = VideoPath("1.mp4"), MediaTime = new TimeOnly(0, 0, 0) },
            new() { MediaName = "Sample Video 2", MediaPath = VideoPath("2.mp4"), MediaTime = new TimeOnly(0, 0, 0) },
            new() { MediaName = "Sample Video 3", MediaPath = VideoPath("3.mp4"), MediaTime = new TimeOnly(0, 0, 0) },
        ];

        private SimpleMediaInfo? _selectedMediaInfo;
        public SimpleMediaInfo? SelectedMediaInfo
        {
            get {
                if(_selectedMediaInfo == null)
                    _selectedMediaInfo = MediaInfos.FirstOrDefault();

                return _selectedMediaInfo; 
            }
            set
            {
                if (ReferenceEquals(_selectedMediaInfo, value))
                    return;

                _selectedMediaInfo = value;
                OnPropertyChanged(nameof(SelectedMediaInfo));
                OnMediaInfoChanged();

            }
        }

        private void OnMediaInfoChanged()
        {
            if (SelectedMediaInfo == null)
                return;

            MediaName = SelectedMediaInfo.MediaName;
            MediaPath = SelectedMediaInfo.MediaPath;
            MediaTime = SelectedMediaInfo.MediaTime;

            MediaStopRequested?.Invoke();

            System.Diagnostics.Debug.WriteLine($"선택 미디어: {SelectedMediaInfo.MediaName}");
        }

        public delegate void MediaPlayRequestedEventHandler();
        public event MediaPlayRequestedEventHandler? MediaPlayRequested;
        public event MediaPlayRequestedEventHandler? MediaPauseRequested;
        public event MediaPlayRequestedEventHandler? MediaStopRequested;

        public void PlayMediaButton_Click(object sender, EventArgs e)
        {
            if (SelectedMediaInfo == null)
                return;

            MediaPlayRequested?.Invoke();
            System.Diagnostics.Debug.WriteLine($"재생 버튼 클릭: {SelectedMediaInfo.MediaName}");
        }

        public void PauseMediaButton_Click(object sender, EventArgs e)
        {
            if (SelectedMediaInfo == null)
                return;

            MediaPauseRequested?.Invoke(); 
            System.Diagnostics.Debug.WriteLine($"일시정지 버튼 클릭: {SelectedMediaInfo.MediaName}");
        }

        public void StopMediaButton_Click(object sender, EventArgs e)
        {
            if (SelectedMediaInfo == null)
                return;

            MediaStopRequested?.Invoke();
            System.Diagnostics.Debug.WriteLine($"정지 버튼 클릭: {SelectedMediaInfo.MediaName}");
        }
    }
}
