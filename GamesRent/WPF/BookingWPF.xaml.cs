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
    /// Logique d'interaction pour Booking.xaml
    /// </summary>
    public partial class BookingWPF : Window
    {
        public Player p;
        public BookingWPF(int idplayer)
        {
            PlayerDAO Pdao = new PlayerDAO();
            p = Pdao.Find(idplayer);
            InitializeComponent();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            BookingSearchByName dashboard = new BookingSearchByName(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void SearchByConsole_Click(object sender, RoutedEventArgs e)
        {
            BookingSearchByConsole dashboard = new BookingSearchByConsole(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void SearchByCrCost_Click(object sender, RoutedEventArgs e)
        {
            BookingSearchByCrCost dashboard = new BookingSearchByCrCost(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void ListBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingList dashboard = new BookingList(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    }
}
