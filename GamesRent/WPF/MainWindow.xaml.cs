using GamesRent.WPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GamesRent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player p;
        public MainWindow(int idplayer)
        {
            PlayerDAO Pdao = new PlayerDAO();
            p = Pdao.Find(idplayer);
            InitializeComponent();
        }
        private void WelcomeLabel_Initialized(object sender, EventArgs e)
        {
            WelcomeLabel.Content = $"Welcome {p.Pseudo.ToUpper()}";
            if(p.DateOfBirth.ToString("d").Substring(1,5)== DateTime.Now.ToString("d").Substring(1, 5))
            {
                WelcomeLabel.Content = $"Welcome {p.Pseudo.ToUpper()} Happy BirthDay !";
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingWPF bookingwindow = new BookingWPF(p.Id_player);
            bookingwindow.Show();
            this.Close();
        }

        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {
            LoanWPF loanwindow = new LoanWPF(p.Id_player);
            loanwindow.Show();
            this.Close();
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            GameWPF gamewindow = new GameWPF(p.Id_player);
            gamewindow.Show();
            this.Close();
        }

        private void btnWallet_Click(object sender, RoutedEventArgs e)
        {
            WalletWPF walletwindow = new WalletWPF(p.Id_player);
            walletwindow.Show();
            this.Close();
        }

        private void btnUnlog_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen loginwindow = new LoginScreen();
            loginwindow.Show();
            this.Close();
        }
    }
}
