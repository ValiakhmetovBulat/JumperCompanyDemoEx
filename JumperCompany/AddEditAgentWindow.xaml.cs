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
    /// Логика взаимодействия для AddEditAgentWindow.xaml
    /// </summary>
    public partial class AddEditAgentWindow : Window
    {
        private Agent _agent;
        private JumperDbContext _db;

        public AddEditAgentWindow(Agent agent)
        {
            InitializeComponent();
            
            _agent = agent;
            DataContext = _agent;
            _db = JumperDbContext.GetContext();

            ComboBoxAgentType.ItemsSource = _db.AgentTypes.ToList();
            ComboBoxDirector.ItemsSource = _db.Directors.ToList();

            if (_agent.AgentId != 0)
            {
                ComboBoxAgentType.SelectedItem = _agent.AgentType;
                ComboBoxDirector.SelectedItem = _agent.Director;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            _agent.AgentType = ComboBoxAgentType.SelectedItem as AgentType;
            _agent.Director = ComboBoxDirector.SelectedItem as Director;

            if (_agent.AgentId == 0)
            {
                _db.Agents.Add(_agent);
            }
            
            try
            {
                _db.SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
