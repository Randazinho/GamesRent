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
            List<Item> Items = new List<Item>();
            List<Game> glist = new List<Game>();
            Game G = new Game();
            glist = G.FindAllGame(glist);
            int games = glist.Count;
            if (games > 0)
            {
                List<Item> items = new List<Item>();
                for (int i = 0; i < games; i++)
                {
                    items.Add(new Item() { Name = glist[i].ToString(),
                    Id =glist[i].Id_game});
                }
                lstGame.ItemsSource = items;
            }
            DataContext = this;
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
                id_game = Convert.ToInt32(SelectedItem.Id);
                NewCrCost = Convert.ToInt32(TxtBoxCreditCost.Text);
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
                TxtBoxCreditCost.Text = "";
            }
        }
        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public Item SelectedItem { get; set; }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = ((RadioButton)sender).DataContext as Item;
        }
    }
}
