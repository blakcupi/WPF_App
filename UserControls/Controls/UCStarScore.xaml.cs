using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControls.Common;

namespace UserControls.Controls
{
    public sealed class StarItem
    {
        public StarItem(int index, bool isFilled)
        {
            Index = index;
            IsFilled = isFilled;
        }

        public int Index { get; }
        public bool IsFilled { get; }
    }

    /// <summary>
    /// UCStarScore.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UCStarScore : UserControl
    {
        public ICommand StarChoiceCommand { get; }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(UCStarScore), 
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, OnCoerceValue));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(UCStarScore),
                new PropertyMetadata(5, OnMaximumChanged));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(UCStarScore),
            new PropertyMetadata(false));

        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            var control = (UCStarScore)d;
            return Math.Max(0, Math.Min(Math.Max(1, control.Maximum), (int)baseValue));
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UCStarScore)d;

            control.RebuildStar();
            ((RelayCommand<int>)control.StarChoiceCommand)?.OnCanExecuteChanged();
        }


        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UCStarScore)d;

            d.CoerceValue(ValueProperty);            
            control.RebuildStar();
        }


        public ObservableCollection<StarItem> Stars { get; } = new ObservableCollection<StarItem>();

        public UCStarScore()
        {
            StarChoiceCommand = new RelayCommand<int>(StarChoice, (index) => index <= Maximum);
            InitializeComponent();

            RebuildStar();
        }

        private void StarChoice(int index)
        {
            if (IsReadOnly)
                return;

            Value = Value == index ? 0 : index;
        }

        private void RebuildStar()
        {
            Stars.Clear();
            for (int i = 1; i <= Maximum; i++)
                Stars.Add(new StarItem(i, i <= Value));
        }
    }
}
