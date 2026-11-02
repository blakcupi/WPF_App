using ListViewStyle.Models;
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

namespace ListViewStyle.Views
{
    /// <summary>
    /// MainWindowView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = (ListView)sender;
            var gridView = (GridView)listView.View;

            // 세로 스크롤바 폭 + 테두리 여유분
            double used = SystemParameters.VerticalScrollBarWidth - 6;

            // 마지막 컬럼을 제외한 폭의 합
            for (int i = 0; i < gridView.Columns.Count - 1; i++)
                used += gridView.Columns[i].ActualWidth;

            double remain = listView.ActualWidth - used;
            if (remain > 0)
                gridView.Columns[^1].Width = remain;   // C# 8+ (Revit 2023/C# 7.3이면 Columns[Columns.Count - 1])
        }
    }

    public class PersonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? ActiveTemplate { get; set; }
        public DataTemplate? InactiveTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return InactiveTemplate;
            else
                return ((PersonModel)item).IsActive ? ActiveTemplate : InactiveTemplate;
        }
    }
}
