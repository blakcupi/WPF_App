using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
    /// <summary>
    /// UCNumericBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UCNumericBox : UserControl
    {
        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double StepValue
        {
            get { return (double)GetValue(StepValueProperty); }
            set { SetValue(StepValueProperty, value); }
        }

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(UCNumericBox), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, OnCoerceValue));

        public static readonly DependencyProperty StepValueProperty =
            DependencyProperty.Register(nameof(StepValue), typeof(double), typeof(UCNumericBox), new PropertyMetadata(1d));

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(nameof(MinValue), typeof(double), typeof(UCNumericBox), new PropertyMetadata(0d, OnRangeChanged));


        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(nameof(MaxValue), typeof(double), typeof(UCNumericBox), new PropertyMetadata(100d, OnRangeChanged));


        public UCNumericBox()
        {
            IncreaseCommand = new RelayCommand(IncreaseValue, () => Value < MaxValue);
            DecreaseCommand = new RelayCommand(DecreaseValue, () => Value > MinValue);

            // XAML의 ElementName 바인딩이 초기화될 때 명령이 이미 존재해야 합니다.
            InitializeComponent();
        }

        private void IncreaseValue()
        {
            Value = Math.Min(Math.Round(Value + StepValue, 10), MaxValue);
        }

        private void DecreaseValue()
        {
            Value = Math.Max(Math.Round(Value - StepValue, 10), MinValue);
        }

        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            var control = (UCNumericBox)d;

            double value = (double)baseValue;
            return Math.Max(control.MinValue, Math.Min(control.MaxValue, value));
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UCNumericBox)d;

            ((RelayCommand)control.IncreaseCommand).OnCanExecuteChanged();
            ((RelayCommand)control.DecreaseCommand).OnCanExecuteChanged();
        }

        private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (UCNumericBox)d;

            d.CoerceValue(ValueProperty);
            ((RelayCommand)control.IncreaseCommand).OnCanExecuteChanged();
            ((RelayCommand)control.DecreaseCommand).OnCanExecuteChanged();
        }


        private void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (IncreaseCommand.CanExecute(null))
                        IncreaseCommand.Execute(null);
                    e.Handled = true;
                    break;

                case Key.Down:
                    if (DecreaseCommand.CanExecute(null))
                        DecreaseCommand.Execute(null);
                    e.Handled = true;
                    break;

                case Key.Enter:
                    InputTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                    e.Handled = true;
                    break;
            }
        }

        private void InputTextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!InputTextBox.IsKeyboardFocused)
                return;

            var command = e.Delta > 0 ? IncreaseCommand : DecreaseCommand;
            if (command.CanExecute(null))
                command.Execute(null);

            e.Handled = true;
        }
    }
}
