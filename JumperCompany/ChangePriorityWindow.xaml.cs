using JumperCompany.Models;
using JumperCompany.Models.Entities;
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

namespace JumperCompany
{
    /// <summary>
    /// Логика взаимодействия для ChangePriorityWindow.xaml
    /// </summary>
    public partial class ChangePriorityWindow : Window
    {
        private List<Agent> _agents;
        private JumperDbContext _db;

        public ChangePriorityWindow(List<Agent> agents)
        {
            InitializeComponent();
            _agents = agents;
            _db = JumperDbContext.GetContext();
        }

        private void ButtonChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _agents)
            {
                if (Int32.TryParse(TextBoxChangePriorityAmount.Text, out var result))
                {
                    item.Priority += result;
                    _db.Update(item);
                }
                else
                {
                    MessageBox.Show("Введите числовое значение");
                    return;
                }
            }
            
            _db.SaveChanges();
            MessageBox.Show("Приоритет агентов изменен");
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();
        }
    }
}
