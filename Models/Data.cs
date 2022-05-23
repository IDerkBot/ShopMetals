namespace Shop.Models
{
    internal class Data
	{
		public static int UserID { get; set; }
		public static int Access { get; set; }
		public static bool IsCustomer() => Access == 0;
		public static bool IsSeller() => Access == 1;
		public static bool IsAdmin() => Access == 2;

		public static string CurrentDirectory = "exporter-m";
		public static string CurrentConfigFile = "config";
		public static string CurrentConfigExtension = "json";
	}
}
