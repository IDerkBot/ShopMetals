using System;
using System.Timers;
using System.Windows;
using Shop.Models;
using ShopMetals.Models;
using ShopMetals.Views.Pages;

namespace ShopMetals.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Timer MainTimer;
		public MainWindow()
		{
			InitializeComponent();
			Manager.SetFrame(MainFrame);
			Manager.Navigate(new Auth());
			Manager.SetWindow(this);
            MainTimer = new Timer(2000);
            MainTimer.Elapsed += TimerOnElapsed;
		}

		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
			Manager.GoBack();
		}

		private void MainFrame_ContentRendered(object sender, EventArgs e)
		{
			BtnBack.Visibility = Manager.CanGoBack() ? Visibility.Visible : Visibility.Hidden;
			BtnCart.Visibility = Manager.CanGoBack() ? Visibility.Visible : Visibility.Hidden;
		}

        private void BtnCart_OnClick(object sender, RoutedEventArgs e) => Manager.Navigate(new CartPage());

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                SbAlert.IsActive = false;
            });
            MainTimer.Stop();
        }
	}
}
