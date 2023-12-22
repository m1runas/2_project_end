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
using System.Windows.Shapes;

namespace Gilmetdinova_eyes_save
{
    /// <summary>
    /// Логика взаимодействия для PriorWindow.xaml
    /// </summary>
    public partial class PriorWindow : Window
    {
        private Agent _currentAgent = new Agent();

        public PriorWindow()
        {
            InitializeComponent();
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(PriorityBox.Text))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введите приоритете агента");
            }
        }

        private void CloseBT_Click(object sender, RoutedEventArgs e)
        {
            PriorityBox.Text = "";
            Close();
        }
    }
}
