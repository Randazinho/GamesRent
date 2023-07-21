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
    /// Logique d'interaction pour AboutMe.xaml
    /// </summary>
    public partial class AboutMe : Window
    {
        public Player p;
        int idplay;
        private Player plr;
        public AboutMe(int idplayer)
        {
            idplay = idplayer;
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void AboutMe_Initialized(object sender, EventArgs e)
        {
            Player P = new Player();
            plr = P.Find(idplay);
            AboutMeContent.Content = plr.ToStringAboutMe();
        }
    }
}
