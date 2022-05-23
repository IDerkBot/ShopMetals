using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shop.Models;
using ShopMetals.Models;
using ShopMetals.Models.Entity;
using ShopMetals.Views.Pages.EditPages;
using ShopMetals.Views.Windows;

namespace ShopMetals.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public SalesPage() => InitializeComponent();
        private void SalesPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<Sale> list;
            list = Data.IsCustomer() ? ShopEntities.GetContext().Sales.Where(x => x.Customer.IDUser == Data.UserID).ToList() : ShopEntities.GetContext().Sales.ToList();
            list.ForEach(x =>
            {
                x.AllItems = string.Join(", ", x.SaleInfoes.Select(y => y.Item.Title));
            });
            DGridSales.ItemsSource = list;
            CbFilter.ItemsSource = ShopEntities.GetContext().TypeItems.ToList();

            #region Отображение элементов

            ColCustomer.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            ColSeller.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            //BtnAdd.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            //CellEdit.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            BtnReport.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;

            #endregion
        }
        #region Buttons

        private void BtnEdit_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new SalesEditPage((sender as Button)?.DataContext as Sale));
        private void BtnDelete_Click(object sender, RoutedEventArgs e) => DGridSales.ItemsSource = Delete.Silk(DGridSales.SelectedItems.Cast<Sale>().ToList());
        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new SalesEditPage());
        private void BtnMore_OnClick(object sender, RoutedEventArgs e) => Manager.Navigate(new SaleInfoPage((sender as Button)?.DataContext as Sale));

        #endregion

        #region Change

        private void CbFilter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DGridSales.ItemsSource = ShopEntities.GetContext().Sales
            //	.Where(x => x.Item.TypeItem.Title == (CbFilter.SelectedItem as TypeItem).Title).ToList();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            DGridSales.ItemsSource = ShopEntities.GetContext().Sales
                .Where(x => x.Customer.Surname.ToLower().Contains((sender as TextBox).Text.ToLower())).ToList();
        }

        #endregion


        private void BtnReport_OnClick(object sender, RoutedEventArgs e)
        {
            new ReportWindow().Show();
        }
    }
}
