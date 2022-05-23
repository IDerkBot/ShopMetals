using System;
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
    /// Логика взаимодействия для ItemsPage.xaml
    /// </summary>
    public partial class ItemsPage : Page
	{
		private bool _view;
		private bool _load;
        public ItemsPage() => InitializeComponent();
        private void ItemsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
			CbSort.ItemsSource = DGridItems.Columns.Select(x => x.Header).ToList();
            CbFilter.ItemsSource = ShopEntities.GetContext().TypeItems.ToList();
			BtnAdd.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            CellEdit.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
            //BtnAdd.Visibility = Data.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
			ColCart.Visibility = Data.IsCustomer() ? Visibility.Visible : Visibility.Collapsed;
			ColBtns.Visibility = Data.IsCustomer() ? Visibility.Visible : Visibility.Collapsed;
			
            _load = true;
        }

        private void LoadData()
        {
            DGridItems.ItemsSource = ShopEntities.GetContext().Items.ToList();
            LvItems.ItemsSource = ShopEntities.GetContext().Items.ToList();
		}
        private void RefreshData()
        {
            var items = DGridItems.ItemsSource.Cast<Item>().ToList();
			DGridItems.ItemsSource = items;
			LvItems.ItemsSource = items;
        }

		#region Buttons

		private void BtnEdit_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new ItemsEditPage((sender as Button)?.DataContext as Item));

		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			if (DGridItems.SelectedItems.Count > 0)
			{
				DGridItems.ItemsSource = Delete.Silk(DGridItems.SelectedItems.Cast<Item>().ToList());
				LvItems.ItemsSource = Delete.Silk(DGridItems.SelectedItems.Cast<Item>().ToList());
			}
			else if (LvItems.SelectedItems.Count > 0)
			{
				DGridItems.ItemsSource = Delete.Silk(LvItems.SelectedItems.Cast<Item>().ToList());
				LvItems.ItemsSource = Delete.Silk(LvItems.SelectedItems.Cast<Item>().ToList());
			}
		}
		private void BtnAdd_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new ItemsEditPage());

		#endregion

        #region Changed
		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var search = (sender as TextBox)?.Text;
			var searchResult = ShopEntities.GetContext().Items
				.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
			DGridItems.ItemsSource = searchResult;
			LvItems.ItemsSource = searchResult;
		}
		private void CbSort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var listForSort = DGridItems.ItemsSource.Cast<Item>().ToList();
			var enumerable = new List<Item>();
			switch (CbSort.SelectedItem.ToString())
			{
				case "Название":
					enumerable = listForSort.OrderBy(x => x.Title).ToList();
					break;
				case "Тип":
					enumerable = listForSort.OrderBy(x => x.TypeItem.Title).ToList();
					break;
				case "Стоимость":
					enumerable = listForSort.OrderBy(x => x.Cost).ToList();
					break;
				case "Количество на складе":
					enumerable = listForSort.OrderBy(x => x.CountInStorage).ToList();
					break;
			}

			DGridItems.ItemsSource = enumerable;
			LvItems.ItemsSource = enumerable;
		}
		private void CbFilter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var filterResult = ShopEntities.GetContext().Items
				.Where(x => x.TypeItem.Title == ((TypeItem) CbFilter.SelectedItem).Title).ToList();
			DGridItems.ItemsSource = filterResult;
			LvItems.ItemsSource = filterResult;
		}
		#endregion
		private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
		{
			if (!_load) return;
			if (_view == false)
			{
				_view = true;
				LvItems.Visibility = Visibility.Visible;
				DGridItems.Visibility = Visibility.Collapsed;
			}
			else
			{
				_view = false;
				LvItems.Visibility = Visibility.Collapsed;
				DGridItems.Visibility = Visibility.Visible;
			}
		}

        private void BtnCart_OnClick(object sender, RoutedEventArgs e)
        {
            if (ShopEntities.GetContext().Customers.Any(x => x.IDUser == Data.UserID))
            {
                var item = (sender as Button)?.DataContext as Item;
				var customer = ShopEntities.GetContext().Customers.Single(x => x.IDUser == Data.UserID);
                if (ShopEntities.GetContext().Sales.Any(x => x.Customer.IDUser == Data.UserID && x.Status.Title == "В корзине"))
                {
                    var sale = ShopEntities.GetContext().Sales.First(x =>
                        x.Customer.IDUser == Data.UserID && x.Status.Title == "В корзине");
                    if (sale.SaleInfoes.Any(x => x.IDItem == item.ID))
                    {
                        sale.SaleInfoes.First(x => x.IDItem == item.ID).Count = item.Count;
                    }
                    else
                    {
						sale.SaleInfoes.Add(new SaleInfo()
                        {
							Count = item.Count,
							Sale = sale,
							Item = item
                        });
                    }

                    ShopEntities.GetContext().SaveChanges();
                }
                else
                {
                    var sale = new Sale
                    {
                        Customer = customer,
                        Status = ShopEntities.GetContext().Status.First(x => x.Title == "В корзине"),
                        Date = DateTime.Now
                    };
                    ShopEntities.GetContext().Sales.Add(sale);
                    ShopEntities.GetContext().SaleInfoes.Add(new SaleInfo()
                    {
                        Item = item,
                        Count = item.Count,
                        Sale = sale
                    });
                    ShopEntities.GetContext().SaveChanges();
				}

                Manager.Alert("Товар добавлен в корзину!");
            }
            else
            {
                var ciaw = new CustomerInfoAddWindow(Data.UserID);
                ciaw.Show();
            }
        }

        private void BtnMinus_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button)?.DataContext as Item;
			if (item != null && item.Count == 0) return;
			item.Count--;
            ((Button)sender).DataContext = item;
            RefreshData();
		}
        private void BtnPlus_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button)?.DataContext as Item;
            if (item != null && item.Count == item.CountInStorage) return;
            item.Count++;
            ((Button)sender).DataContext = item;
            RefreshData();
		}
	}
}