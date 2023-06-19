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
    /// Logique d'interaction pour BookingList.xaml
    /// </summary>
    public partial class BookingList : Window
    {
        public Player p;
        public BookingList(int idplayer)
        {
            PlayerDAO Pdao = new PlayerDAO();
            p = Pdao.Find(idplayer);
            InitializeComponent();
        }
        private void Booking_Initialized(object sender, EventArgs e)
        {
            try
            {
                List<Booking> blist = new List<Booking>();
                BookingDAO BDAO = new BookingDAO();
                blist = BDAO.FindAllBookingByPlayerID(blist, p.Id_player);
                string concats = "";
                foreach (Booking b in blist)
                {
                    concats += b.ToString() + "\n";
                }
                Bookings.Content = concats.Substring(0, concats.Length -1);
            }
            catch
            {
                Bookings.Content = "No booking for the moment";
            }
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
                idbook = Convert.ToInt32(TxtBoxId.Text);
                MessageBox.Show(" " + idbook);
                if (idbook > 0)
                {

                    BookingDAO BDAO = new BookingDAO();
                    BDAO.Delete(idbook);
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
                TxtBoxId.Text = "";
            }
        }
    }
}
