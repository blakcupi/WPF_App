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

namespace LayoutTest
{
    /// <summary>
    /// LayoutTestView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LayoutTestView : Window
    {
        public LayoutTestView()
        {
            InitializeComponent();
        }

        private void GridTestButton_Click(object sender, RoutedEventArgs e)
        {
            GridTestView gridTestView = new GridTestView();
            gridTestView.ShowDialog();
        }

        private void StackPanelTestButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanelTestView stackPanelTestView = new StackPanelTestView();
            stackPanelTestView.ShowDialog();
        }

        private void DockPanelTestButton_Click(object sender, RoutedEventArgs e)
        {
            DockPanelTestView dockPanelTestView = new DockPanelTestView();
            dockPanelTestView.ShowDialog();
        }

        private void CanvasTestButton_Click(object sender, RoutedEventArgs e)
        {
            CanvasTestView canvasTestView = new CanvasTestView();
            canvasTestView.ShowDialog();    
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New menu item clicked");
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open menu item clicked");
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save menu item clicked");
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save As menu item clicked");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exit menu item clicked");
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About menu item clicked");
        }

        private void ContextMenuItem1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Grid Test context menu item clicked");
        }

    }
}
