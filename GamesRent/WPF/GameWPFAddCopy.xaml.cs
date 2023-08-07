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
            LoadGames();
        }
        private void LoadGames()
        {
            List<Game> glist = new List<Game>();
            Game G = new Game();
            glist = G.FindAllGame(glist);
            GamesDataGrid.ItemsSource = glist;
        }

        private void AddCopy_Click(object sender, RoutedEventArgs e)
        {
            int id_game = 0;
            try
            {
                Game selectedGame = (Game)GamesDataGrid.SelectedItem;
                id_game = Convert.ToInt32(selectedGame.Id_game);
                if (id_game>0)
                {
                    Copy C = new Copy();
                    int idcopy =C.CreateCopy(id_game,p.Id_player);
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
                        P.UpdateWallet(p.Id_player, G.Find(id_game).CreditCost * week, "+");
                        MessageBox.Show("Someone had already booked this game, loan created");
                        //recharge la page pour la nouvelle liste de copies
                        GameWPF dashboard = new GameWPF(p.Id_player);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No-one wants to rent this game at the moment, it has been added to your list of copies");
                        //recharge la page pour la nouvelle liste de copies
                        GameWPF dashboard = new GameWPF(p.Id_player);
                        dashboard.Show();
                        this.Close();
                    }
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            GameWPF dashboard = new GameWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void GamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesDataGrid.SelectedItem != null)
            {
                Game selectedGame = (Game)GamesDataGrid.SelectedItem;
                MessageBox.Show("Game selected");
            }
        }
    }
}
