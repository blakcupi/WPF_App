using SimpleMediaViewer.Models;
using SimpleMediaViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleMediaViewer.Views
{
    /// <summary>
    /// SimpleViewerView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SimpleViewerView : Window
    {
        public SimpleViewerView()
        {
            InitializeComponent();

            var vm = (SimpleViewerViewModel)DataContext;

            vm.MediaPlayRequested += () => mediaElement.Play();
            vm.MediaPauseRequested += () => mediaElement.Pause();
            vm.MediaStopRequested += () => mediaElement.Stop();
        }
    }
}
