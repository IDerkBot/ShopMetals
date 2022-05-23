using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shop.Models;
using ShopMetals.Models;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
	{
		public Auth() => InitializeComponent();
		private void Auth_Click(object sender, RoutedEventArgs e)
		{
			if (ShopEntities.GetContext().Users.Any(x => x.Login == TbLogin.Text))
			{
				var user = ShopEntities.GetContext().Users.First(x => x.Login == TbLogin.Text);
				if (CbRemember.IsChecked == true) FileManager.SetConfig(new Config(user.Login, user.Password, true));
				if (user.Password == PbPassword.Password)
				{
					Data.Access = user.Access;
					Data.UserID = user.ID;
					Manager.Navigate(new MenuPage());
				}
				else
					MessageBox.Show("Пароль не верный");
			}
			else
				MessageBox.Show("Такого пользователя не существует");
		}
		private void Reg_Click(object sender, RoutedEventArgs e) => Manager.Navigate(new Reg());
		private void Auth_OnLoaded(object sender, RoutedEventArgs e)
		{
			if (!FileManager.GetConfig().IsRemember) return;
			var config = FileManager.GetConfig();
			TbLogin.Text = config.Login;
			PbPassword.Password = config.Password;
			CbRemember.IsChecked = true;
		}
	}
}