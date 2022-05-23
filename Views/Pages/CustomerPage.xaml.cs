using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shop.Models;
using ShopMetals.Models;
using ShopMetals.Models.Entity;
using ShopMetals.Views.Pages.EditPages;

namespace ShopMetals.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для CustomerPage.xaml
	/// </summary>
	public partial class CustomerPage : Page
	{
		public CustomerPage() => InitializeComponent();
		private void CustomerPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DGridCustomers.ItemsSource = ShopEntities.GetContext().Customers.ToList();
			CbSort.ItemsSource = DGridCustomers.Columns.Select(x => x.Header).ToList();

			BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
		}

		#region Buttons

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e) => Manager.Navigate(new CustomerEditPage((sender as Button)?.DataContext as Customer));
		private void BtnDelete_OnClick(object sender, RoutedEventArgs e) => DGridCustomers.ItemsSource = Delete.Silk(DGridCustomers.SelectedItems.Cast<Customer>().ToList());
		private void BtnAdd_OnClick(object sender, RoutedEventArgs e) => Manager.Navigate(new CustomerEditPage());

		#endregion

		#region Change

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var listForSort = DGridCustomers.ItemsSource.Cast<Customer>().ToList();
			var enumerable = new List<Customer>();
			switch (CbSort.SelectedItem.ToString())
			{
				case "ФИО":
					enumerable = listForSort.OrderBy(x => x.Fullname).ToList();
					break;
				case "Телефон":
					enumerable = listForSort.OrderBy(x => x.Phone).ToList();
					break;
				case "Почта":
					enumerable = listForSort.OrderBy(x => x.Email).ToList();
					break;
			}

			DGridCustomers.ItemsSource = enumerable;
		}
		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var searchResult = new List<Customer>();
			var searchText = ((TextBox) sender).Text;
			ShopEntities.GetContext().Customers.ToList().ForEach(x =>
			{
				if(x.Fullname.ToLower().Contains(searchText) ||
				   (string.IsNullOrWhiteSpace(x.Phone) == false && x.Phone.Contains(searchText)) ||
				   (string.IsNullOrWhiteSpace(x.Email) == false && x.Email.Contains(searchText)))
					searchResult.Add(x);
			});
			DGridCustomers.ItemsSource = searchResult;
		}

		#endregion
	}
}