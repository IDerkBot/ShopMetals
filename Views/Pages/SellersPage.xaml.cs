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
	/// Логика взаимодействия для SellersPage.xaml
	/// </summary>
	public partial class SellersPage : Page
	{
		private bool _view;
		private bool _load;

		#region Load

		public SellersPage() => InitializeComponent();
		private void SellersPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DGridSellers.ItemsSource = ShopEntities.GetContext().Sellers.ToList();
			LvSellers.ItemsSource = ShopEntities.GetContext().Sellers.ToList();
			CbSort.ItemsSource = DGridSellers.Columns.Select(x => x.Header).ToList();

			BtnAdd.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			CellEdit.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			_load = true;
		}

		#endregion

		#region Buttons

		private void BtnEdit_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new SellerEditPage((sender as Button)?.DataContext as Seller));
		private void BtnDelete_Click(object sender, RoutedEventArgs e) => DGridSellers.ItemsSource = Delete.Silk(DGridSellers.SelectedItems.Cast<Seller>().ToList());
		private void BtnAdd_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new SellerEditPage());

		#endregion

		#region Change

		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var listForSort = DGridSellers.ItemsSource.Cast<Seller>().ToList();
			var enumerable = new List<Seller>();
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

			DGridSellers.ItemsSource = enumerable;
		}
		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			DGridSellers.ItemsSource = ShopEntities.GetContext().Sellers
				.Where(x => x.Surname.ToLower().Contains((sender as TextBox).Text.ToLower())).ToList();
		}

		#endregion

		private void RbView_OnChecked(object sender, RoutedEventArgs e)
		{
			if (!_load) return;
			if (_view == false)
			{
				_view = true;
				LvSellers.Visibility = Visibility.Visible;
				DGridSellers.Visibility = Visibility.Collapsed;
			}
			else
			{
				_view = false;
				LvSellers.Visibility = Visibility.Collapsed;
				DGridSellers.Visibility = Visibility.Visible;
			}
		}
	}
}
