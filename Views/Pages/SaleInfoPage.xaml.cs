using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShopMetals.Models.Entity;

namespace ShopMetals.Views.Pages
{
    /// <summary>
    /// Interaction logic for SaleInfoPage.xaml
    /// </summary>
    public partial class SaleInfoPage : Page
    {
        private readonly Sale _currentSale;
        public SaleInfoPage(Sale selectedSale)
        {
            InitializeComponent();
            _currentSale = selectedSale;
            DataContext = _currentSale;
        }
        private void SaleInfoPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            LvItems.ItemsSource = ShopEntities.GetContext().Sales.First(x => x.ID == _currentSale.ID)
                .SaleInfoes.ToList();
        }
    }
}
