using JumperCompany.Models;
using JumperCompany.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JumperCompany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Agent> _agents;
        public MainWindow()
        {
            InitializeComponent();
            _agents = JumperDbContext.GetContext().Agents.ToList();
            listViewAgents.ItemsSource = _agents;

            List<AgentType> agentTypes = new List<AgentType>
            {
                new AgentType() { TypeName = "Все типы" }
            };
            agentTypes.AddRange(JumperDbContext.GetContext().AgentTypes);
            comboAgentTypesFilter.ItemsSource = agentTypes;

            comboAgentSort.ItemsSource = new List<string>
            {
                "Все", 
                "По имени по возрастанию", 
                "По имени по убыванию", 
                "По приоритету по возрастанию", 
                "По приоритету по убыванию",
                "По скидке по возрастанию",
                "По скидке по убыванию"
            };

            comboAgentTypesFilter.SelectedIndex = 0;
            comboAgentSort.SelectedIndex = 0;
        }

        private void comboAgentTypesFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortAgents();
        }

        private void SortAgents()
        {
            var sorted = _agents;

            sorted = FilterByAgentTypes(sorted);
            sorted = SearchAgentsByText(sorted);
            sorted = SortAgents(sorted);

            listViewAgents.ItemsSource = sorted;
        }

        private List<Agent> FilterByAgentTypes(List<Agent> agents)
        {
            if (comboAgentTypesFilter.SelectedIndex != 0 )
            {
                agents = agents.Where(a => a.AgentType == comboAgentTypesFilter.SelectedItem).ToList();
            }

            return agents;
        }

        private List<Agent> SortAgents(List<Agent> agents)
        {
            switch (comboAgentSort.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }                    
                case 1:
                    {
                        agents = agents.OrderBy(a => a.AgentName).ToList();
                        break;
                    }
                case 2:
                    {
                        agents = agents.OrderByDescending(a => a.AgentName).ToList();
                        break;
                    }
                case 3:
                    {
                        agents = agents.OrderBy(a => a.Priority).ToList();
                        break;
                    }
                case 4:
                    {
                        agents = agents.OrderByDescending(a => a.Priority).ToList();
                        break;
                    }
                case 5:
                    {
                        agents = agents.OrderBy(a => a.AgentDiscountAmount).ToList();
                        break;
                    }
                case 6:
                    {
                        agents = agents.OrderByDescending(a => a.AgentDiscountAmount).ToList();
                        break;
                    }
            }
            return agents;
        }


        private List<Agent> SearchAgentsByText(List<Agent> agents)
        {
            var searchText = textBoxSearchAgents.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                agents = agents.
                    Where(a => 
                    a.AgentName.ToLower().Contains(searchText) ||
                    a.Email.ToLower().Contains(searchText) ||
                    a.Phone.ToLower().Contains(searchText)
                    ).ToList();
            }

            return agents;
        }

        private void textBoxSearchAgents_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortAgents();
        }

        private void ButtonAddAgent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboAgentSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortAgents();
        }

        private void ButtonDeleteAgent_Click(object sender, RoutedEventArgs e)
        {
            var selectedAgents = listViewAgents.SelectedItems.Cast<Agent>().ToList();

            if (selectedAgents.Count == 0)
            {
                MessageBox.Show("Выберите агента, которого желаете удалить нажатием на карточку");
                return;
            }
            if ((MessageBox.Show("Выбранный агент будет удален. Продолжить?", "Удаление агента", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                var db = JumperDbContext.GetContext();
                db.Agents.RemoveRange(selectedAgents);
                db.SaveChanges();
                MessageBox.Show("Агент был успешно удален");
                _agents = db.Agents.ToList();
                listViewAgents.ItemsSource = _agents;
                SortAgents();
            }
        }
    }
}
