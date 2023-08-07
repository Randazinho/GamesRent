using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static GamesRent.WPF.Rating;

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
            LoadGames();
        }
        private void LoadGames()
        {
            List<Game> glist = new List<Game>();
            Game G = new Game();
            glist = G.FindAllGame(glist);
            GamesDataGrid.ItemsSource = glist;
        }

        private void AdminGameMainMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminGame dashboard = new AdminGame();
            dashboard.Show();
            this.Close();
        }
        private void ModifyGame_Click(object sender, RoutedEventArgs e)
        {
            int id_game,NewCrCost;
            try
            {
                Game selectedGame = (Game)GamesDataGrid.SelectedItem;
                id_game = Convert.ToInt32(selectedGame.Id_game);
                NewCrCost = Convert.ToInt32(comboBoxNumbers.Text);
                if (id_game > 0 & NewCrCost > 0)
                {
                    //MessageBox.Show(id_game + " " + NewCrCost);
                    Game G = new Game();
                    G.UpdateCostByID(id_game, NewCrCost);
                    MessageBox.Show("The Credit Cost has been updated");
                    //recharge la page pour afficher le nouveau creditcost
                    AdminModifyGame dashboard = new AdminModifyGame();
                    dashboard.Show();
                    this.Close();
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Select a game");
            }
        }
        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private void GamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesDataGrid.SelectedItem != null)
            {
                Game selectedGame = (Game)GamesDataGrid.SelectedItem;
                MessageBox.Show("Game selected");
            }
        }

        private void ComboBoxNumbers_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (comboBoxNumbers.SelectedItem != null)
            {
                int selectedNumber = (int)comboBoxNumbers.SelectedItem;
            }
        }
    }
}
