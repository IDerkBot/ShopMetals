using ShopMetals.Models.Entity;
using System.Linq;
using System.Windows;

namespace ShopMetals.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void BtnDo_OnClick(object sender, RoutedEventArgs e)
        {
            if (DpStart.SelectedDate == null || DpEnd.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты");
                return;
            }

            var list = ShopEntities.GetContext().Sales.Where(x => x.Date >= DpStart.SelectedDate && x.Date <= DpEnd.SelectedDate).ToList();
            TblCount.Text = $"Количество продаж: {list.Count}";
            TblSum.Text = $"Сумма продаж: {list.Sum(x => x.SaleInfoes.Sum(y => y.Item.Cost))}";
        }
    }
}
