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
    /// Logique d'interaction pour GameWPFAddCopy.xaml
    /// </summary>
    public partial class GameWPFAddCopy : Window
    {
        public Player p;
        Game G = new Game();
        Loan L = new Loan();
        Player P = new Player();
        Booking B = new Booking();
        public GameWPFAddCopy(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
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

        private void AddCopy_Click(object sender, RoutedEventArgs e)
        {
            int id_game = 0;
            try
            {
                id_game = Convert.ToInt32(TxtBoxIdGame.Text);
                if (id_game>0)
                {
                    Copy C = new Copy();
                    int idcopy =C.CreateCopy(id_game,p.Id_player);
                    //recharge la page pour la nouvelle liste de copies
                    GameWPFAddCopy dashboard = new GameWPFAddCopy(p.Id_player);
                    dashboard.Show();
                    this.Close();
                    //selectbooking
                    int idplayerborrower = G.SelectBooking(id_game);
                    if(idplayerborrower!=0)//on peut créer la loan
                    {
                        //Select le nombre week de la booking 
                        int week = B.FindWeekByIDPlayer(idplayerborrower,id_game);
                        L.CreateLoan(idcopy,idplayerborrower,week); 
                        //empecher de book plusieurs fois le même jeu par un même player en même temps
                        Booking book = B.FindABookByIdGameAndIDPlayer(id_game, idplayerborrower);
                        B.DeleteBooking(book.Id_booking);
                        P.UpdateWalletByID(idplayerborrower, G.Find(idcopy).CreditCost, p.Id_player);
                        MessageBox.Show("Wallet uptaded");
                    }
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error in the information filled in");
                TxtBoxIdGame.Text = "";
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            GameWPF dashboard = new GameWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    }
}
