using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;

        public AgentPage()
        {
            InitializeComponent();

            var currentAgent = Gilmetdinova_eyesEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgent;


            ComboType2.SelectedIndex = 0;
            ComboType.SelectedIndex = 0;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UpdateAgent();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAgent();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAgent();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAgent();
        }

        private void UpdateAgent()
        {
            var currentAgent = Gilmetdinova_eyesEntities.GetContext().Agent.ToList();

            if (ComboType.SelectedIndex == 0)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID >= 1 && p.AgentTypeID <= 6)).ToList();
            }

            if (ComboType.SelectedIndex == 1)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 1)).ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 2)).ToList();
            }

            if (ComboType.SelectedIndex == 3)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 3)).ToList();
            }

            if (ComboType.SelectedIndex == 4)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 4)).ToList();
            }

            if (ComboType.SelectedIndex == 5)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 5)).ToList();
            }

            if (ComboType.SelectedIndex == 6)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID == 6)).ToList();
            }




            if (ComboType2.SelectedIndex == 0)
            {
                currentAgent = currentAgent.Where(p => (p.AgentTypeID >= 1 && p.AgentTypeID <= 6)).ToList();
            }

            if (ComboType2.SelectedIndex == 1)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Priority).ToList();
            }

            if (ComboType2.SelectedIndex == 2)
            {
                currentAgent = currentAgent.OrderBy(p => p.Priority).ToList();
            }

            if (ComboType2.SelectedIndex == 3)
            {
                currentAgent = currentAgent.OrderByDescending(p => p.Title).ToList();
            }

            if (ComboType2.SelectedIndex == 4)
            {
                currentAgent = currentAgent.OrderBy(p => p.Title).ToList();
            }

            if (ComboType2.SelectedIndex == 5)
            {
                //  currentAgent = currentAgent.OrderByDescending(p => p.Скидка).ToList();
            }

            if (ComboType2.SelectedIndex == 6)
            {
                //   currentAgent = currentAgent.OrderBy(p => p.Скидка).ToList();
            }


            currentAgent = currentAgent.Where(p =>
            p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.Replace("+7", "7").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Contains(TBoxSearch.Text.Replace("+7", "8").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")) || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            AgentListView.ItemsSource = currentAgent;

            TableList = currentAgent;

            ChangePage(0, 0);
        }

        private void ComboType2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgent();
        }


        private void ChangePage(int diraction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (diraction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }

                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                }
            }
        
            if (Ifupdate)
            {
                PageListBox.Items.Clear();

                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }

                AgentListView.ItemsSource = CurrentPageList;

                AgentListView.Items.Refresh();
            }
        }
    

        



        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
            
        }

        private void RightDitButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
          
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString())-1);
            
        }

        private void AddEditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void AddPage_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAgent();
        }

        

        private void ChangePriory_Click(object sender, RoutedEventArgs e)
        {

            PriorWindow window = new PriorWindow();
            window.ShowDialog();
            if (string.IsNullOrEmpty(window.PriorityBox.Text))
            {
                return;
            }
            foreach (Agent AgentLV in AgentListView.SelectedItems)
            {
                AgentLV.Priority = Convert.ToInt32(window.PriorityBox.Text);
            }
            try
            {
                Gilmetdinova_eyesEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateAgent();
        }

       

        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AgentListView.SelectedItems.Count > 1)
            {
                ChangePriory.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriory.Visibility = Visibility.Hidden;
            }
        }
    }
    }

