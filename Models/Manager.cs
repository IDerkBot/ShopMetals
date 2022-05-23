using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using ShopMetals.Views.Windows;

namespace ShopMetals.Models
{
    internal class Manager
	{
        private static Frame MainFrame { get; set; }
        public static void SetFrame(Frame frame)
        {
            MainFrame = frame;
        }
        public static void Navigate(Page moveToPage)
        {
            MainFrame.Navigate(moveToPage);
        }
        public static void GoBack()
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }
        public static bool CanGoBack()
        {
            return MainFrame.CanGoBack;
        }
        public static Page GetPage()
        {
            return MainFrame.Content as Page;
        }

        private static MainWindow MainWindow { get; set; }
        public static void SetWindow(MainWindow window)
        {
            MainWindow = window;
        }
        public static void Alert(string message)
        {
            MainWindow.SbAlert.Message = new SnackbarMessage { Content = message };
            MainWindow.SbAlert.IsActive = true;
            MainWindow.MainTimer.Start();
        }
    }
}
