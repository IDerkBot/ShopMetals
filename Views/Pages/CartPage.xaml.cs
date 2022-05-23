using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shop.Models;
using ShopMetals.Models;
using ShopMetals.Models.Entity;
using ShopMetals.Views.Windows;

namespace ShopMetals.Views.Pages
{
    /// <summary>
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        private readonly Sale _currentSale;
        private readonly List<Status> _listStatus;
        public CartPage()
        {
            InitializeComponent();
            _listStatus = ShopEntities.GetContext().Status.ToList();
            GetCart();
            _currentSale = ShopEntities.GetContext().Sales
                .First(x => x.Customer.IDUser == Data.UserID && x.Status.Title == "В корзине");
            DgCart.ItemsSource = _currentSale.SaleInfoes.ToList();
        }

        private void CartPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
        }

        private void GetCart()
        {
                if (ShopEntities.GetContext().Customers.Any(x => x.IDUser == Data.UserID))
                {
                    var customer = ShopEntities.GetContext().Customers.Single(x => x.IDUser == Data.UserID);
                    if (ShopEntities.GetContext().Sales
                        .Any(x => x.Customer.IDUser == Data.UserID && x.Status.Title == "В корзине"))
                    {
                        var sale = ShopEntities.GetContext().Sales.First(x =>
                            x.Customer.IDUser == Data.UserID && x.Status.Title == "В корзине");

                        ShopEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        var sale = new Sale
                        {
                            Customer = customer,
                            Status = ShopEntities.GetContext().Status.First(x => x.Title == "В корзине"),
                            Date = DateTime.Now
                        };
                        ShopEntities.GetContext().Sales.Add(sale);
                        ShopEntities.GetContext().SaveChanges();
                    }
                }
                else
                {
                        var ciaw = new CustomerInfoAddWindow(Data.UserID);
                        ciaw.Show();
                }
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _currentSale.Status = _listStatus.FirstOrDefault(x => x.Title == "Ожидает оплаты");
            //MessageBox.Show("Заказ оформлен");
            ShopEntities.GetContext().SaveChanges();
            Manager.GoBack();
            Manager.Alert("Заказ оформлен");
        }
    }
}