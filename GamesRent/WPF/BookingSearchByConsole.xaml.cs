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
using static GamesRent.WPF.AdminModifyGame;

namespace GamesRent.WPF
{
    /// <summary>
    /// Logique d'interaction pour BookingSearchByConsole.xaml
    /// </summary>
    public partial class BookingSearchByConsole : Window
    {
        public Player p;
        string selectedConsoleName;
        private List<ConsoleItem> consoleList;
          public List<ConsoleItem> ConsoleList { get; } = new List<ConsoleItem>
        {
            new ConsoleItem { Console = "XBOX SERIES", Group = "XBOX" },
            new ConsoleItem { Console = "XBOX ONE", Group = "XBOX" },
            new ConsoleItem { Console = "XBOX 360", Group = "XBOX" },
            new ConsoleItem { Console = "PLAYSTATION 5", Group = "PLAYSTATION" },
            new ConsoleItem { Console = "PLAYSTATION 4", Group = "PLAYSTATION" },
            new ConsoleItem { Console = "PLAYSTATION 3", Group = "PLAYSTATION" },
            new ConsoleItem { Console = "SWITCH", Group = "NINTENDO" },
            new ConsoleItem { Console = "WII", Group = "NINTENDO" },
            new ConsoleItem { Console = "DS", Group = "NINTENDO" }
        };

        public BookingSearchByConsole(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            CreateNumberList();
            DataContext = this;
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
                        int amount = game.CreditCost * week;
                        p.UpdateWallet(p.Id_player, amount,"-");
                        //MessageBox.Show("ID de la booking : " + id_booking);
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
                MessageBox.Show("Select a game and the number of weeks");
                BookGame.Visibility = Visibility.Collapsed;
                numberListBox.Visibility = Visibility.Collapsed;
                LabelWeeks.Visibility = Visibility.Collapsed;
                LabelConsole.Visibility = Visibility.Visible;
                comboBoxConsole.Visibility = Visibility.Visible;
                Search.Visibility = Visibility.Visible;
                GamesDataGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (selectedConsoleName.Count() > 0)
            {
                string console = selectedConsoleName;
                List<Game> glist = new List<Game>();
                Game G = new Game();
                glist = G.FindGameByConsole(console, glist);
                int games = glist.Count;
                if (games > 0)
                {
                    GamesDataGrid.ItemsSource = glist;
                    DataContext = this;
                    BookGame.Visibility = Visibility.Visible;
                    numberListBox.Visibility = Visibility.Visible;
                    LabelWeeks.Visibility = Visibility.Visible;
                    LabelConsole.Visibility = Visibility.Collapsed;
                    comboBoxConsole.Visibility = Visibility.Collapsed;
                    Search.Visibility = Visibility.Collapsed;
                    GamesDataGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("No Console found");
                    GamesDataGrid.ItemsSource = null;
                    BookGame.Visibility = Visibility.Collapsed;
                    numberListBox.Visibility = Visibility.Collapsed;
                    LabelWeeks.Visibility = Visibility.Collapsed;
                    LabelConsole.Visibility = Visibility.Visible;
                    comboBoxConsole.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Fill in at least one character");
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
        private void ComboBoxConsole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxConsole.SelectedItem != null)
            {
                var selectedConsole = comboBoxConsole.SelectedItem as ConsoleItem;
                if (selectedConsole != null)
                {
                    selectedConsoleName = selectedConsole.Console;
                }
            }
        }
    }
}
