using ShopMetals.Models;
using ShopMetals.Models.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShopMetals.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
	{
		public Reg() => InitializeComponent();

		private void Reg_Click(object sender, RoutedEventArgs e)
		{
			if (ShopEntities.GetContext().Users.Any(x => x.Login == TbLogin.Text))
			{
				MessageBox.Show("Пользователь уже существует");
				return;
			}
			ShopEntities.GetContext().Users.Add(new User{Login =TbLogin.Text, Password = PbPassword.Password});
			ShopEntities.GetContext().SaveChanges();
			Manager.GoBack();
		}
	}
}