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
            GameDAO GDAO = new GameDAO();
            glist = GDAO.FindAllGame(glist);
            string concats = "";
            foreach (Game g in glist)
            {
                concats += g.ToString() + "\n";
            }
            Games.Content = concats.Substring(0, concats.Length - 1);
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            int idgame = 0;
            try
            {
                idgame = Convert.ToInt32(TxtBoxId.Text);
                MessageBox.Show(" "+idgame);
                if (idgame > 0)
                {
                    GameDAO GDAO = new GameDAO();
                    GDAO.DeleteGame(idgame);
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
                TxtBoxId.Text = "";
            }
        }
    }
}
