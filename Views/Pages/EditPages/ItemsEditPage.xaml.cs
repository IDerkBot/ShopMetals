using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ShopMetals.Models;
using ShopMetals.Models.Entity;
using form =  System.Windows.Forms;

namespace ShopMetals.Views.Pages.EditPages
{
	/// <summary>
	/// Логика взаимодействия для ItemsEditPage.xaml
	/// </summary>
	public partial class ItemsEditPage : Page
	{
		private readonly Item _currentItem;
		public ItemsEditPage()
		{
			InitializeComponent();
			_currentItem = new Item();
			DataContext = _currentItem;
			GetTypeItems();
		}
		public ItemsEditPage(Item selectedItem)
		{
			InitializeComponent();
			_currentItem = selectedItem;
			DataContext = _currentItem;
			GetTypeItems();
		}
		private void GetTypeItems()
		{
			CbType.ItemsSource = ShopEntities.GetContext().TypeItems.ToList();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			_currentItem.Title = TbTitle.Text;
			_currentItem.Type = ShopEntities.GetContext().TypeItems.ToList().Where(x => x.Title == (CbType.SelectedItem as TypeItem)?.Title).Select(x => x.ID).First();
			_currentItem.Cost = decimal.Parse(TbCost.Text);
			if (_currentItem.ID == 0) ShopEntities.GetContext().Items.Add(_currentItem);
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
			_currentItem.Image = File.ReadAllBytes(filePath[0]);
			var ms = new MemoryStream(_currentItem.Image);
			var source = new BitmapImage();
			source.BeginInit();
			source.StreamSource = ms;
			source.EndInit();
			ImageView.Source = source;
		}

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
			form.OpenFileDialog fd = new form.OpenFileDialog();
			fd.ShowDialog();
        }
    }
}
