using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShopMetals.Models;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Pages.EditPages
{
	/// <summary>
	/// Логика взаимодействия для SalesEditPage.xaml
	/// </summary>
	public partial class SalesEditPage : Page
	{
		private readonly Sale _currentSale;
		public SalesEditPage()
		{
			InitializeComponent();
			GetData();
			_currentSale = new Sale { Date = DateTime.Now };
			DataContext = _currentSale;
		}
		public SalesEditPage(Sale selectedSale)
		{
			InitializeComponent();
			_currentSale = selectedSale;
			DataContext = _currentSale;
			GetData();
		}
		private void GetData()
		{
			//CbItem.ItemsSource = ShopEntities.GetContext().Items.ToList();
			LvItems.ItemsSource = ShopEntities.GetContext().Items.ToList();
			CbSeller.ItemsSource = ShopEntities.GetContext().Sellers.ToList();
			CbCustomer.ItemsSource = ShopEntities.GetContext().Customers.ToList();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(CbCustomer.SelectedItem == null || CbSeller.SelectedItem == null) return;
			foreach (var item1 in LvItems.SelectedItems.Cast<Item>())
			{
				//_currentSale.Items.Add(item1);
				_currentSale.SaleInfoes.Add(new SaleInfo() {Item = item1, Count = 1});
			}
			if (!ShopEntities.GetContext().Sales.Any(x => x.ID == 0))
				ShopEntities.GetContext().Sales.Add(_currentSale);
			var item = ShopEntities.GetContext().Items.First(x => x.Title == ((Item) LvItems.SelectedItem).Title);
			//item.CountInStorage -= _currentSale.Count;
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			Manager.GoBack();
		}

		private void TbCount_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			//Проверить количество товара на складе и если нет, то вывести сколько осталось на складе
			if(LvItems.SelectedItem == null) return;

			var item = ShopEntities.GetContext().Items.ToList().First(x => x.Title == (LvItems.SelectedItem as Item)?.Title);
			if (TbCount.Text == "") return;
			TblSum.Text = $"{item.Cost * int.Parse(TbCount.Text)} рублей";
			if (int.Parse((sender as TextBox)?.Text ?? "0") <= item.CountInStorage) return;
			MessageBox.Show($"Внимание!\nНа складе осталось {item.CountInStorage} шт.");
			((TextBox) sender).Text = $"{item.CountInStorage}";
		}
	}
}
