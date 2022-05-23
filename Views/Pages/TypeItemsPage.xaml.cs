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
	/// Логика взаимодействия для TypeItemsPage.xaml
	/// </summary>
	public partial class TypeItemsPage : Page
	{
		public TypeItemsPage() => InitializeComponent();
		private void BtnEdit_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new TypeItemsEditPage((sender as Button)?.DataContext as TypeItem));
		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			var typeItemsForRemoving = DGridTypeItems.SelectedItems.Cast<TypeItem>().ToList();
			if (MessageBox.Show($"Вы точно хотите удалить следующие {typeItemsForRemoving.Count()} элементов?", "Внимание",
				    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
			ShopEntities.GetContext().TypeItems.RemoveRange(typeItemsForRemoving);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			DGridTypeItems.ItemsSource = ShopEntities.GetContext().TypeItems.ToList();
		}
		private void BtnAdd_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new TypeItemsEditPage());

		private void TypeItemsPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			DGridTypeItems.ItemsSource = ShopEntities.GetContext().TypeItems.ToList();

			BtnAdd.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			BtnDelete.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
			CellEdit.Visibility = Data.IsAdmin() ? Visibility.Visible : Visibility.Collapsed;
		}
		private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			DGridTypeItems.ItemsSource = ShopEntities.GetContext().TypeItems
				.Where(x => x.Title.ToLower().Contains((sender as TextBox).Text.ToLower())).ToList();
		}
	}
}
