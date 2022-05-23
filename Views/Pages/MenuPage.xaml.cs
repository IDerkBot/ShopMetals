using System.Windows;
using System.Windows.Controls;
using Shop.Models;
using ShopMetals.Models;

namespace ShopMetals.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage() => InitializeComponent();

        private void Items_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new ItemsPage());

        private void Sellers_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new SellersPage());

        private void Customer_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new CustomerPage());

        private void TypeItems_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new TypeItemsPage());

        private void Sales_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new SalesPage());

        private void MenuPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            BtnSellers.Visibility = Visibility.Collapsed;
            if (Data.IsCustomer())
            {
                BtnCustomers.Visibility = Visibility.Collapsed;
                BtnSellers.Visibility = Visibility.Collapsed;
                BtnTypeItems.Visibility = Visibility.Collapsed;
                BtnSales.Content = "Мои покупки";
            }
        }
    }
}