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
        GameDAO GDAO = new GameDAO();
        LoanDAO LDAO = new LoanDAO();
        PlayerDAO PDAO = new PlayerDAO();
        BookingDAO BDAO = new BookingDAO();
        public GameWPFAddCopy(int idplayer)
        {
            PlayerDAO Pdao = new PlayerDAO();
            p = Pdao.Find(idplayer);
            InitializeComponent();
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

        private void AddCopy_Click(object sender, RoutedEventArgs e)
        {
            int id_game = 0;
            try
            {
                id_game = Convert.ToInt32(TxtBoxIdGame.Text);
                if (id_game>0)
                {
                    CopyDAO CDAO = new CopyDAO();
                    int idcopy =CDAO.CreateCopy(id_game,p.Id_player);
                    //recharge la page pour la nouvelle liste de copies
                    GameWPFAddCopy dashboard = new GameWPFAddCopy(p.Id_player);
                    dashboard.Show();
                    this.Close();
                    //selectbooking
                    int idplayerborrower = GDAO.SelectBooking(id_game);
                    if(idplayerborrower!=0)//on peut créer la loan
                    {
                        //Select le nombre week de la booking 
                        int week = BDAO.FindWeekByIDPlayer(idplayerborrower,id_game);
                        LDAO.CreateLoan(idcopy,idplayerborrower,week); 
                        //empecher de book plusieurs fois le même jeu par un même player en même temps
                        Booking book = BDAO.FindABookByIdGameAndIDPlayer(id_game, idplayerborrower);
                        BDAO.Delete(book.Id_booking);
                        PDAO.UpdateWalletByID(idplayerborrower, GDAO.Find(idcopy).CreditCost, p.Id_player);
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
