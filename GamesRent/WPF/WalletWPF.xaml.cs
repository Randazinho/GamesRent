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
    /// Logique d'interaction pour Wallet.xaml
    /// </summary>
    public partial class WalletWPF : Window
    {
        public Player p;
        int idplay;
        int wallet = 0;
        private Player plr;
        public WalletWPF(int idplayer)
        {
            idplay = idplayer;
            Player P = new Player();
            p = P.Find(idplayer);
            wallet = p.Credit;
            InitializeComponent();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void Credit_Initialized(object sender, EventArgs e)
        {
            Player P = new Player();
            plr = P.Find(idplay);
            wallet = plr.Credit;
            Credit.Content = wallet;
            if (p.Credit <= 0)
            {
                MessageBox.Show("You must now rent your games to get credits before you can create a new booking");
                Credit.Foreground = Brushes.Red;
            }
        }
    }
}
