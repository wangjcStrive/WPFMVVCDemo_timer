using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer.ViewModels;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TimerView : Window
    {
        #region Fields
        TimerViewModel _viewModel;
        #endregion

        public TimerView()
        {
            InitializeComponent();
            this.MaximizeToSecondaryMonitor();
            ViewModel.Context = this;
            _viewModel = base.DataContext as TimerViewModel;
        }


        public void MaximizeToSecondaryMonitor()
        {
            var secondaryScreen = Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            if (secondaryScreen != null)
            {
                var workingArea = secondaryScreen.WorkingArea;
                this.Left = workingArea.Left;
                this.Top = workingArea.Top;
                this.Width = workingArea.Width;
                this.Height = workingArea.Height;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
