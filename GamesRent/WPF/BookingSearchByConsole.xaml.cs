﻿using System;
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
        public BookingSearchByConsole(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            CreateNumberList();
            BookGame.Visibility = Visibility.Collapsed;
            numberListBox.Visibility = Visibility.Collapsed;
            LabelWeeks.Visibility = Visibility.Collapsed;
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
                int idgame = Convert.ToInt32(SelectedItem.Id);
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
                        p.UpdateWalletForBooking(p.Id_player, amount,"-");
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
                TxtBoxConsole.Text = "";
                BookGame.Visibility = Visibility.Collapsed;
                numberListBox.Visibility = Visibility.Collapsed;
                LabelWeeks.Visibility = Visibility.Collapsed;
                LabelConsole.Visibility = Visibility.Visible;
                TxtBoxConsole.Visibility = Visibility.Visible;
                Search.Visibility = Visibility.Visible;
                lstGame.ItemsSource = null;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBoxConsole.Text.Count() > 0)
            {
                string console = TxtBoxConsole.Text;
                List<Game> glist = new List<Game>();
                Game G = new Game();
                glist = G.FindGameByConsole(console, glist);
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
                    DataContext = this;
                    BookGame.Visibility = Visibility.Visible;
                    numberListBox.Visibility = Visibility.Visible;
                    LabelWeeks.Visibility = Visibility.Visible;
                    LabelConsole.Visibility = Visibility.Collapsed;
                    TxtBoxConsole.Visibility = Visibility.Collapsed;
                    Search.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("No Console found");
                    lstGame.ItemsSource = null;
                    BookGame.Visibility = Visibility.Collapsed;
                    numberListBox.Visibility = Visibility.Collapsed;
                    LabelWeeks.Visibility = Visibility.Collapsed;
                    LabelConsole.Visibility = Visibility.Visible;
                    TxtBoxConsole.Visibility = Visibility.Visible;
                    TxtBoxConsole.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Fill in at least one character");
            }
        }
        public Item SelectedItem { get; set; }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = ((RadioButton)sender).DataContext as Item;
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
    }
}
