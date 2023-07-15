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
using static GamesRent.WPF.AdminModifyGame;

namespace GamesRent.WPF
{
    /// <summary>
    /// Logique d'interaction pour AdminDeleteGame.xaml
    /// </summary>
    public partial class AdminDeleteGame : Window
    {
        public AdminDeleteGame()
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
                    items.Add(new Item()
                    {
                        Name = glist[i].ToString(),
                        Id = glist[i].Id_game
                    });
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

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            int idgame = 0;
            try
            {

                idgame = Convert.ToInt32(SelectedItem.Id);
                //MessageBox.Show(" "+idgame);
                if (idgame > 0)
                {
                    Game G = new Game();
                    G.DeleteGame(idgame);
                    //recharge la page pour afficher la nouvelle liste
                    AdminGame dashboard = new AdminGame();
                    dashboard.Show();
                    this.Close();
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Pick a game to delete");
            }
        }
        public Item SelectedItem { get; set; }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = ((RadioButton)sender).DataContext as Item;
        }
    }
}
