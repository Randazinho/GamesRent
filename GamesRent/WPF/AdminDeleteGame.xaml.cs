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
    /// Logique d'interaction pour AdminDeleteGame.xaml
    /// </summary>
    public partial class AdminDeleteGame : Window
    {
        public AdminDeleteGame()
        {
            InitializeComponent();
            List<Game> glist = new List<Game>();
            Game G = new Game();
            glist = G.FindAllGame(glist);
            int games = glist.Count;
            if (games > 0)
            {
                List<IdValue> items = new List<IdValue>();
                for (int i = 0; i < games; i++)
                {
                    items.Add(new IdValue() { Value = glist[i].Id_game.ToString() });
                }

            }
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
            G.FindAllGame(glist);
            string concats = "";
            foreach (Game g in glist)
            {
                concats += g.ToString() + "\n";
            }
   
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            int idgame = 0;
            try
            {

                idgame = Convert.ToInt32((lstID.SelectedItem as IdValue).Value);
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
                MessageBox.Show("Error with the Id selected");
            }
        }
        public class IdValue
        {
            public string Value { get; set; }
        }
    }
}
