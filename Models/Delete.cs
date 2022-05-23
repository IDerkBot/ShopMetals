using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ShopMetals.Models.Entity;

namespace ShopMetals.Models
{
	internal class Delete
	{
		public static List<Item> Silk(List<Item> itemsForRemoving)
		{
			if (MessageBox.Show($"Вы точно хотите удалить следующие {itemsForRemoving.Count} элементов?", "Внимание",
				    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return ShopEntities.GetContext().Items.ToList();
			ShopEntities.GetContext().Items.RemoveRange(itemsForRemoving.ToList());
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			return ShopEntities.GetContext().Items.ToList();
		}
		public static List<Seller> Silk(List<Seller> sellersForRemoving)
		{
			if (MessageBox.Show($"Вы точно хотите удалить следующие {sellersForRemoving.Count()} элементов?", "Внимание",
				    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return ShopEntities.GetContext().Sellers.ToList();
			ShopEntities.GetContext().Sellers.RemoveRange(sellersForRemoving);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			return ShopEntities.GetContext().Sellers.ToList();
		}
		public static List<Sale> Silk(List<Sale> salesForRemoving)
		{
			if (MessageBox.Show($"Вы точно хотите удалить следующие {salesForRemoving.Count()} элементов?", "Внимание",
				    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return ShopEntities.GetContext().Sales.ToList();
			ShopEntities.GetContext().Sales.RemoveRange(salesForRemoving);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			return ShopEntities.GetContext().Sales.ToList();
		}
		public static List<TypeItem> Silk(List<TypeItem> typeItemsForRemoving)
		{
			if (MessageBox.Show($"Вы точно хотите удалить следующие {typeItemsForRemoving.Count()} элементов?", "Внимание",
				    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return ShopEntities.GetContext().TypeItems.ToList();
			ShopEntities.GetContext().TypeItems.RemoveRange(typeItemsForRemoving);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			return ShopEntities.GetContext().TypeItems.ToList();
		}
		public static List<Customer> Silk(List<Customer> customersForRemoving)
		{
			if (MessageBox.Show($"Вы точно хотите удалить следующие {customersForRemoving.Count()} элементов?", "Внимание",
				    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return ShopEntities.GetContext().Customers.ToList();
			ShopEntities.GetContext().Customers.RemoveRange(customersForRemoving);
			ShopEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные удалены!");
			return ShopEntities.GetContext().Customers.ToList();
		}
	}
}
