using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Logique d'interaction pour BookingSearchByConsole.xaml
    /// </summary>
    public partial class BookingSearchByConsole : Window
    {
        public Player p;
        public BookingSearchByConsole(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            BookGame.Visibility = Visibility.Collapsed;
            TxtBoxId.Visibility = Visibility.Collapsed;
            LabelID.Visibility = Visibility.Collapsed;
            TxtWeeks.Visibility = Visibility.Collapsed;
            LabelWeeks.Visibility = Visibility.Collapsed;
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            BookingWPF dashboard = new BookingWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void Games_Initialized(object sender, EventArgs e)
        {
            Games.Content = "A game for which platform?";
        }

        private void BookGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idgame = Convert.ToInt32(TxtBoxId.Text);
                int week = Convert.ToInt32(TxtWeeks.Text);
                if (idgame>0 & (week>0 & week<53))
                {
                    Booking B = new Booking();
                    int id_booking = 0;
                    Game G = new Game();
                    Game game = G.Find(idgame);
                    int loanAllowed = p.LoanAllowed(p.Id_player, idgame, week);
                    if (loanAllowed==1)
                    {
                        id_booking = B.CreateBookingByIdGame(p.Id_player, idgame, week);
                        MessageBox.Show("ID de la booking : " + id_booking);
                        //ici check si copie available
                        int flag = G.CopyAvailable(idgame, p.Id_player, id_booking,week);
                        if (flag == 0)
                        {
                            BookingList dashboard = new BookingList(p.Id_player);
                            dashboard.Show();
                            this.Close();
                        }
                        else
                        {
                            LoanWPFInProgress dashboard = new LoanWPFInProgress(p.Id_player);
                            dashboard.Show();
                            this.Close();
                        }
                    }
                    else if (loanAllowed==2)
                    {
                        MessageBox.Show("This game is already in your booking list");
                        BookingList dashboard = new BookingList(p.Id_player);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Not enough credit to book this game");
                        WalletWPF dashboard = new WalletWPF(p.Id_player);
                        dashboard.Show();
                        this.Close();
                    }
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error in the information filled in");
                TxtBoxConsole.Text = "";
                TxtBoxId.Text = "";
                BookGame.Visibility = Visibility.Collapsed;
                TxtBoxId.Visibility = Visibility.Collapsed;
                LabelID.Visibility = Visibility.Collapsed;
                TxtWeeks.Visibility = Visibility.Collapsed;
                LabelWeeks.Visibility = Visibility.Collapsed;
                Games.Content = "";
                LabelConsole.Visibility = Visibility.Visible;
                TxtBoxConsole.Visibility = Visibility.Visible;
                Search.Visibility = Visibility.Visible;
                TxtWeeks.Text = "";
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(TxtBoxConsole.Text.Count()>0)
            {
                string console = TxtBoxConsole.Text;
                List<Game> glist = new List<Game>();
                Game G = new Game();
                glist = G.FindGameByConsole(console, glist);
                string concats = "";
                if (glist.Count > 0)
                {
                    foreach (Game g in glist)
                    {
                        concats += g.ToString() + "\n";
                    }
                    Games.Content = concats.Substring(0, concats.Length);
                    BookGame.Visibility = Visibility.Visible;
                    TxtBoxId.Visibility = Visibility.Visible;
                    LabelID.Visibility = Visibility.Visible;
                    TxtWeeks.Visibility = Visibility.Visible;
                    LabelWeeks.Visibility = Visibility.Visible;
                    LabelConsole.Visibility = Visibility.Collapsed;
                    TxtBoxConsole.Visibility = Visibility.Collapsed;
                    Search.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Games.Content = "No Console found";
                    BookGame.Visibility = Visibility.Collapsed;
                    TxtBoxId.Visibility = Visibility.Collapsed;
                    LabelID.Visibility = Visibility.Collapsed;
                    LabelConsole.Visibility = Visibility.Visible;
                    TxtBoxConsole.Visibility = Visibility.Visible;
                    TxtWeeks.Visibility = Visibility.Collapsed;
                    LabelWeeks.Visibility = Visibility.Collapsed;
                    TxtBoxConsole.Text = "";
                    TxtWeeks.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Fill in at least one character");
            }
        }
    }
}
