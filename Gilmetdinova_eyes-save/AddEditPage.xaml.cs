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
using Microsoft.Win32;

namespace Gilmetdinova_eyes_save
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    /// 
    public partial class AddEditPage : Page
    {
        private Agent currentAgent = new Agent();
        List<AgentType> agentTypeDBList = Gilmetdinova_eyesEntities.GetContext().AgentType.ToList();
        public AddEditPage(Agent SelectedAgent=null)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                currentAgent = SelectedAgent;
                Combotype.SelectedIndex = currentAgent.AgentTypeID - 1;
            }
            else
                Prodagi.Visibility= Visibility.Hidden;
            DataContext = currentAgent;
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                currentAgent.Logo = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");
            if (Combotype.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            else
            {
                currentAgent.AgentTypeID = Combotype.SelectedIndex + 1;
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(")", "").Replace(" ", "");
                if (ph.Length > 1)
                {
                    if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 10) || (ph[1] == '3' && ph.Length != 11))
                        errors.AppendLine("Укажите правильно телефон агента");
                }
                else if (ph[0] != 8 || ph[0] != 7) errors.AppendLine("Укажите правильно телефон агента");
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Email))
                errors.AppendLine("Укажите почту агента");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentAgent.ID == 0)
            {
                Gilmetdinova_eyesEntities.GetContext().Agent.Add(currentAgent);
            }
            try
            {
                Gilmetdinova_eyesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {


            var currentAgent = (sender as Button).DataContext as Agent;

            var curruntProductSale = Gilmetdinova_eyesEntities.GetContext().ProductSale.ToList();
            curruntProductSale = curruntProductSale.Where(p => p.AgentID == currentAgent.ID).ToList();

            if (curruntProductSale.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существует реализация продукции");

            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Gilmetdinova_eyesEntities.GetContext().Agent.Remove(currentAgent);
                        Gilmetdinova_eyesEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Prodagi_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SalePage((sender as Button).DataContext as Agent));
        }
    }
}
