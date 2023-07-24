using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour BookingList.xaml
    /// </summary>
    public partial class BookingList : Window
    {
        public Player p;
        public BookingList(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            List<Item> Items = new List<Item>();
            List<Booking> blist = new List<Booking>();
            Booking B = new Booking();
            blist = B.FindAllBookingByPlayerID(blist, p.Id_player);
            int booking = blist.Count;
            if (booking > 0)
            {
                List<Item> items = new List<Item>();
                for (int i = 0; i < booking; i++)
                {
                    items.Add(new Item()
                    {
                        Name = blist[i].ToString(),
                        Id = blist[i].Id_booking
                    });
                }
                lstBooking.ItemsSource = items;
            }
            else
            {
                MessageBox.Show("No booking for the moment");
                DeleteBooking.Visibility = Visibility.Collapsed;
            }
            DataContext = this;
        }
        
        private void BookingMainMenu_Click(object sender, RoutedEventArgs e)
        {
            BookingWPF dashboard = new BookingWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void DeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            int idbook = 0;
            try
            {
                idbook = Convert.ToInt32(SelectedItem.Id);
                MessageBox.Show(" " + idbook);
                if (idbook > 0)
                {
                    Booking B = new Booking();
                    B= B.Find(idbook);
                    Game G = new Game();
                    G= G.Find(B.Game.Id_game);
                    int amount = G.CreditCost * B.Week;
                    p.UpdateWalletForBooking(p.Id_player, amount, "+");
                    B.DeleteBooking(idbook);
                    //recharge la page pour afficher la nouvelle liste
                    BookingList dashboard = new BookingList(p.Id_player);
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
        public Item SelectedItem { get; set; }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = ((RadioButton)sender).DataContext as Item;
        }
    }
}
