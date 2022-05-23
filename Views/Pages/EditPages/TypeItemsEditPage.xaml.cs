using System.Windows;
using System.Windows.Controls;
using ShopMetals.Models;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Pages.EditPages
{
	/// <summary>
	/// Логика взаимодействия для TypeItemsPage.xaml
	/// </summary>
	public partial class TypeItemsEditPage : Page
	{
		private readonly TypeItem _currentTypeItem;
		public TypeItemsEditPage()
		{
			InitializeComponent();
			_currentTypeItem = new TypeItem();
			DataContext = _currentTypeItem;
		}
		public TypeItemsEditPage(TypeItem selectedTypeItem)
		{
			InitializeComponent();
			_currentTypeItem = selectedTypeItem;
			DataContext = _currentTypeItem;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (_currentTypeItem.ID == 0) ShopEntities.GetContext().TypeItems.Add(_currentTypeItem);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены");
			Manager.GoBack();
		}
	}
}
