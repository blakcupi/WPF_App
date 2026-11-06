using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UserControls.Common;

namespace UserControls.Controls
{
    /// <summary>
    /// UCSearchBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UCSearchBox : UserControl
    {
        private readonly RelayCommand _clearCommand;

        public ICommand ClearCommand => _clearCommand;

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(UCSearchBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
            nameof(Watermark), typeof(string), typeof(UCSearchBox), 
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SearchCommandProperty = DependencyProperty.Register(
            nameof(SearchCommand), typeof(ICommand), typeof(UCSearchBox), 
            new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }


        public UCSearchBox()
        {
            _clearCommand = new RelayCommand(() => { Text = string.Empty; searchTextBox.Focus(); }, () => !string.IsNullOrEmpty(Text));

            InitializeComponent();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UCSearchBox)d)._clearCommand.OnCanExecuteChanged(); // Text 변경 시 ClearCommand의 CanExecute 상태를 갱신
        }

        private void SearchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var command = SearchCommand;
                if (command != null && command.CanExecute(Text))
                    command.Execute(Text);

                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                Text = string.Empty;

                e.Handled = true;
            }
        }

        private void ExecuteSearch()
        {
            ////// IME 조합 중인 글자까지 반영되도록 TextBox의 현재 값을 DP로 강제 동기화
            ////searchTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            var text = searchTextBox.Text;   // IME 조합 중인 글자까지 포함된 현재 값
            Text = text;                     // DP도 함께 동기화

            var command = SearchCommand;
            if (command != null && command.CanExecute(text))
                command.Execute(text);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => ExecuteSearch();
    }
}
