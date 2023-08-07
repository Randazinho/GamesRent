using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Logique d'interaction pour BookingSearchByCrCost.xaml
    /// </summary>
    public partial class BookingSearchByCrCost : Window
    {
        public Player p;
        public BookingSearchByCrCost(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            CreateNumberList();
            BookGame.Visibility = Visibility.Collapsed;
            numberListBox.Visibility = Visibility.Collapsed;
            LabelWeeks.Visibility = Visibility.Collapsed;
            GamesDataGrid.Visibility = Visibility.Collapsed;
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            BookingWPF dashboard = new BookingWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    
        private void BookGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Game selectedGame = (Game)GamesDataGrid.SelectedItem;
                int idgame = Convert.ToInt32(selectedGame.Id_game);
                int week = (int)numberListBox.SelectedItem;
                if (idgame > 0 & (week > 0 & week < 53))
                {
                    Booking B = new Booking();
                    int id_booking = 0;
                    Game G = new Game();
                    Game game = G.Find(idgame);
                    int loanAllowed = p.LoanAllowed(p.Id_player, idgame, week);
                    if (loanAllowed == 1)
                    {
                        id_booking = B.CreateBookingByIdGame(p.Id_player, idgame,week);
                        int amount = game.CreditCost * week;
                        p.UpdateWallet(p.Id_player, amount,"-");
                        //MessageBox.Show("ID de la booking : " + id_booking);
                        //ici check si copie available
                        int flag = G.CopyAvailable(idgame, p.Id_player, id_booking, week);
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
                    else if (loanAllowed == 2)
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
                MessageBox.Show("Select a game and the number of weeks");
                BookGame.Visibility = Visibility.Collapsed;
                numberListBox.Visibility = Visibility.Collapsed;
                LabelWeeks.Visibility = Visibility.Collapsed;
                LabelCreditCost.Visibility = Visibility.Visible;
                comboBoxNumbers.Visibility = Visibility.Visible;
                Search.Visibility = Visibility.Visible;
                GamesDataGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxNumbers.Text.Count() > 0)
            {
                int crCost = 0;
                try
                {
                    crCost = Convert.ToInt32(comboBoxNumbers.Text);
                    List<Game> glist = new List<Game>();
                    Game G = new Game();
                    glist = G.FindGameByCrCost(crCost, glist);
                    int games = glist.Count;
                    if (games > 0)
                    {
                        GamesDataGrid.ItemsSource = glist;
                        DataContext = this;
                        BookGame.Visibility = Visibility.Visible;
                        numberListBox.Visibility = Visibility.Visible;
                        LabelWeeks.Visibility = Visibility.Visible;
                        LabelCreditCost.Visibility = Visibility.Collapsed;
                        comboBoxNumbers.Visibility = Visibility.Collapsed;
                        Search.Visibility = Visibility.Collapsed;
                        GamesDataGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("No games found");
                        GamesDataGrid.Visibility = Visibility.Collapsed;
                        BookGame.Visibility = Visibility.Collapsed;
                        numberListBox.Visibility = Visibility.Collapsed;
                        LabelWeeks.Visibility = Visibility.Collapsed;
                        LabelCreditCost.Visibility = Visibility.Visible;
                        comboBoxNumbers.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                    MessageBox.Show("Error in the information filled in");
                    BookGame.Visibility = Visibility.Collapsed;
                    LabelWeeks.Visibility = Visibility.Collapsed;
                    LabelCreditCost.Visibility = Visibility.Visible;
                    comboBoxNumbers.Visibility = Visibility.Visible;
                    GamesDataGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Fill a maximum credit cost");
            }
        }
        private void GamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesDataGrid.SelectedItem != null)
            {
                Game selectedGame = (Game)GamesDataGrid.SelectedItem;
                MessageBox.Show("Game selected");
            }
        }
        private void CreateNumberList()
        {
            List<int> numbers = new List<int>();

            for (int i = 1; i <= 52; i++)
            {
                numbers.Add(i);
            }

            numberListBox.ItemsSource = numbers;
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
