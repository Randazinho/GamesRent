using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        public class MultiplicationConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values.Length >= 2 && values[0] is int week && values[1] is int creditCost)
                {
                    return (week * creditCost).ToString();
                }
                return "";
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
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
                BookingDataGrid.ItemsSource = blist;
            }
            else
            {
                MessageBox.Show("No booking for the moment");
                DeleteBooking.Visibility = Visibility.Collapsed;
                BookingDataGrid.Visibility = Visibility.Collapsed;
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
                Booking selectedBook = (Booking)BookingDataGrid.SelectedItem;
                idbook = Convert.ToInt32(selectedBook.Id_booking);
                //MessageBox.Show(" " + idbook);
                if (idbook > 0)
                {
                    Booking B = new Booking();
                    B= B.Find(idbook);
                    Game G = new Game();
                    G= G.Find(B.Game.Id_game);
                    int amount = G.CreditCost * B.Week;
                    p.UpdateWallet(p.Id_player, amount, "+");
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
                MessageBox.Show("Select a booking");
            }
        }
        private void BookingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingDataGrid.SelectedItem != null)
            {
                Booking selectedBooking = (Booking)BookingDataGrid.SelectedItem;
                MessageBox.Show("Game selected");
            }
        }
    }
}
