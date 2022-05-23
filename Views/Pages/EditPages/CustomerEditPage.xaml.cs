using System.Windows;
using System.Windows.Controls;
using ShopMetals.Models;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Pages.EditPages
{
	/// <summary>
	/// Логика взаимодействия для CustomerEditPage.xaml
	/// </summary>
	public partial class CustomerEditPage : Page
	{
		private readonly Customer _currentCustomer;
		public CustomerEditPage()
		{
			InitializeComponent();
			_currentCustomer = new Customer();
			DataContext = _currentCustomer;
		}
		public CustomerEditPage(Customer selectedCustomer)
		{
			InitializeComponent();
			_currentCustomer = selectedCustomer;
			DataContext = _currentCustomer;
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentCustomer.Surname) || string.IsNullOrWhiteSpace(_currentCustomer.Firstname) ||
			    string.IsNullOrWhiteSpace(_currentCustomer.Patronymic))
			{
				MessageBox.Show("Заполните обязательные поля: Фамилия, имя, отчество");
				return;
			}

			if (_currentCustomer.ID == 0) ShopEntities.GetContext().Customers.Add(_currentCustomer);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Клиент успешно сохранен!");
			Manager.GoBack();
		}
	}
}
