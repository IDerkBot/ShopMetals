using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ShopMetals.Models;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Pages.EditPages
{
	/// <summary>
	/// Логика взаимодействия для SellerEditPage.xaml
	/// </summary>
	public partial class SellerEditPage : Page
	{
		private readonly Seller _currentSeller;
		public SellerEditPage()
		{
			InitializeComponent();
			_currentSeller = new Seller();
			DataContext = _currentSeller;
		}
		public SellerEditPage(Seller selectedSeller)
		{
			InitializeComponent();
			_currentSeller = selectedSeller;
			DataContext = _currentSeller;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentSeller.Surname) || string.IsNullOrWhiteSpace(_currentSeller.Firstname) ||
			    string.IsNullOrWhiteSpace(_currentSeller.Patronymic))
			{
				MessageBox.Show("Заполните все обязательные поля: Фамилия, имя, отчество");
				return;
			}
			if (_currentSeller.Code == 0) ShopEntities.GetContext().Sellers.Add(_currentSeller);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			Manager.GoBack();
		}

		private void UIElement_OnDrop(object sender, DragEventArgs e)
		{
			SpView.Visibility = Visibility.Collapsed;
			ImageView.Visibility = Visibility.Visible;
			var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (filePath == null) return;
			_currentSeller.Image = File.ReadAllBytes(filePath[0]);
			var ms = new MemoryStream(_currentSeller.Image);
			var source = new BitmapImage();
			source.BeginInit();
			source.StreamSource = ms;
			source.EndInit();
			ImageView.Source = source;
		}
	}
}
