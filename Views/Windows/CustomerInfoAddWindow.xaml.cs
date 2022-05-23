using System.Windows;
using ShopMetals.Models;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Windows
{
    /// <summary>
    /// Interaction logic for CustomerInfoAddWindow.xaml
    /// </summary>
    public partial class CustomerInfoAddWindow : Window
    {
        public CustomerInfoAddWindow(int userId)
        {
            InitializeComponent();
            CurrentCustomer = new Customer { IDUser = userId };
            DataContext = CurrentCustomer;
        }
        public Customer CurrentCustomer { get; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbSurname.Text) || string.IsNullOrWhiteSpace(TbFirstname.Text) || string.IsNullOrWhiteSpace(TbPatronymic.Text))
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            ShopEntities.GetContext().Customers.Add(CurrentCustomer);
            ShopEntities.GetContext().SaveChanges();
            Manager.Alert("Данные сохранены!\nПриятной покупки");
            Close();
        }
    }
}
