using System.Windows;
using System.Windows.Controls;

namespace UserControls.Controls
{
    /// <summary>
    /// 로딩 오버레이. 같은 Grid 셀에서 대상 콘텐츠 위에 겹쳐 배치한다.
    /// IsBusy=true 일 때만 나타나며 아래 콘텐츠로의 마우스 입력을 막는다.
    /// 사용: &lt;c:UCBusyIndicator IsBusy="{Binding IsBusy}" Message="불러오는 중..." /&gt;
    /// </summary>
    public partial class UCBusyIndicator : UserControl
    {
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(UCBusyIndicator),
                new PropertyMetadata(false));

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(UCBusyIndicator),
                new PropertyMetadata("처리 중입니다..."));

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public UCBusyIndicator()
        {
            InitializeComponent();
        }
    }
}
