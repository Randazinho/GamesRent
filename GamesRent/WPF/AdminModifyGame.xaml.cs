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
    /// Logique d'interaction pour AdminModifyGame.xaml
    /// </summary>
    public partial class AdminModifyGame : Window
    {
        public AdminModifyGame()
        {
            InitializeComponent();
        }
        private void AdminGameMainMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminGame dashboard = new AdminGame();
            dashboard.Show();
            this.Close();
        }
        private void Games_Initialized(object sender, EventArgs e)
        {
            List<Game> glist = new List<Game>();
            Game G = new Game();
            glist = G.FindAllGame(glist);
            string concats = "";
            foreach (Game g in glist)
            {
                concats += g.ToString() + "\n";
            }
            Games.Content = concats.Substring(0, concats.Length - 1);
        }
        private void ModifyGame_Click(object sender, RoutedEventArgs e)
        {
            int id_game,NewCrCost;
            try
            {
                id_game = Convert.ToInt32(TxtBoxIdGame.Text);
                NewCrCost = Convert.ToInt32(TxtBoxCreditCost.Text);
                if (id_game > 0 & NewCrCost > 0)
                {
                    //MessageBox.Show(id_game + " " + NewCrCost);
                    Game G = new Game();
                    G.UpdateCostByID(id_game, NewCrCost);
                    //recharge la page pour afficher le nouveau creditcost
                    AdminModifyGame dashboard = new AdminModifyGame();
                    dashboard.Show();
                    this.Close();
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error with the Id selected or with the new CreditCost");
                TxtBoxCreditCost.Text = "";
                TxtBoxIdGame.Text = "";
            }
        }
    }
}
