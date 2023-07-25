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

namespace GamesRent.WPF
{
    /// <summary>
    /// Logique d'interaction pour AdminAddGame.xaml
    /// </summary>
    public class ConsoleItem
    {
        public string Console { get; set; }
        public string Group { get; set; }
    }
    public partial class AdminAddGame : Window
    {
        string selectedConsoleName;
        public List<ConsoleItem> ConsoleList { get; } = new List<ConsoleItem>
        {
            new ConsoleItem { Console = "XBOX SERIES", Group = "XBOX" },
            new ConsoleItem { Console = "XBOX ONE", Group = "XBOX" },
            new ConsoleItem { Console = "XBOX 360", Group = "XBOX" },
            new ConsoleItem { Console = "PLAYSTATION 5", Group = "PLAYSTATION" },
            new ConsoleItem { Console = "PLAYSTATION 4", Group = "PLAYSTATION" },
            new ConsoleItem { Console = "PLAYSTATION 3", Group = "PLAYSTATION" },
            new ConsoleItem { Console = "SWITCH", Group = "NINTENDO" },
            new ConsoleItem { Console = "WII", Group = "NINTENDO" },
            new ConsoleItem { Console = "DS", Group = "NINTENDO" }
        };
        public AdminAddGame()
        {
            InitializeComponent();
            DataContext = this; 
        }

        private void AdminGameMainMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminGame dashboard = new AdminGame();
            dashboard.Show();
            this.Close();
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            int crCost;
            string name;
            try
            {
                crCost = Convert.ToInt32(comboBoxNumbers.Text);
                name = TxtBoxName.Text;
                if (crCost > 0 & name != "")
                {
                    Game G = new Game();
                    G.CreateGameByAdmin(name, crCost, selectedConsoleName);
                    //recharge la page pour afficher le nouveau jeu
                    MessageBox.Show("Game added");
                    AdminGame dashboard = new AdminGame();
                    dashboard.Show();
                    this.Close();
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error in the information filled in");
                comboBoxConsole.Text = "";
                TxtBoxName.Text = "";
            }

        }
        private void ComboBoxNumbers_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (comboBoxNumbers.SelectedItem != null)
            {
                int selectedNumber = (int)comboBoxNumbers.SelectedItem;
            }
        }
        private void ComboBoxConsole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxConsole.SelectedItem != null)
            {
                var selectedConsole = comboBoxConsole.SelectedItem as ConsoleItem;
                if (selectedConsole != null)
                {
                    selectedConsoleName = selectedConsole.Console;
                }
            }
        }
    }
}
