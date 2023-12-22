using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gilmetdinova_eyes_save
{
    /// <summary>
    /// Логика взаимодействия для AddSalePage.xaml
    /// </summary>
    public partial class AddSalePage : Page
    {
        private ProductSale currentProductSale = new ProductSale();
        public List<Product> currentProduct = new List<Product>(Gilmetdinova_eyesEntities.GetContext().Product.ToList());
        Agent currentAgent;
        
        public AddSalePage(Agent SelectedAgent)
        {
            InitializeComponent();
            currentAgent = SelectedAgent;
            ProductsComboBox.ItemsSource = currentProduct;
            DataContext = currentProductSale;
        }

        private void SaveSaleButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (ProductsComboBox.SelectedItem == null)
                error.AppendLine("Выберите продукт");
            if (Convert.ToInt32(ProductCount.Text) == 0)
                error.AppendLine("Введите корректное количество продукта");
            if (ProductSaleDate.SelectedDate == null)
                error.AppendLine("Введите дату");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            currentProductSale.ID = 0;
            currentProductSale.AgentID = currentAgent.ID;
            currentProductSale.ProductID = currentProduct[ProductsComboBox.SelectedIndex].ID;
            currentProductSale.SaleDate = Convert.ToDateTime(ProductSaleDate.Text);
            currentProductSale.ProductCount = Convert.ToInt32(ProductCount.Text);

            Gilmetdinova_eyesEntities.GetContext().ProductSale.Add(currentProductSale);
            Gilmetdinova_eyesEntities.GetContext().SaveChanges();


            MessageBox.Show("информация сохранена");
            Manager.MainFrame.GoBack();
        }

        private void ProductsComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProductsComboBox.IsDropDownOpen = true;
            currentProduct = currentProduct.Where(p => p.Title.ToLower().Contains(ProductsComboBox.Text.ToLower())).ToList();
            ProductsComboBox.ItemsSource = currentProduct;
        }
    }
}
