namespace Shop.Models
{
	internal class Config
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public bool IsRemember { get; set; }

		public Config()
		{

		}
		public Config(string login, string password, bool isRemember)
		{
			Login = login;
			Password = password;
			IsRemember = isRemember;
		}
	}
}