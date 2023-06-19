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
    public partial class AdminAddGame : Window
    {
        public AdminAddGame()
        {
            InitializeComponent();
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
            string name, console;
            try
            {
                crCost = Convert.ToInt32(TxtBoxCrediCost.Text);
                name = TxtBoxName.Text;
                console= TxtBoxConsole.Text;
                if (crCost > 0 & name!="" & console!="")
                {
                    GameDAO GDAO = new GameDAO();
                    GDAO.CreateGameByAdmin(name,crCost, console);
                    //recharge la page pour afficher le nouveau creditcost
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
                TxtBoxConsole.Text = "";
                TxtBoxName.Text = "";
                TxtBoxCrediCost.Text = "";
            }
        }
    }
}
