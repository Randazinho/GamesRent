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
            DrawStars();
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
        private void DrawStars()
        {
            double starSize = 30;
            int filledStars = (int)((p.Rating/p.Nbr_rater)/2); // Nombre d'étoiles entières
            double remainingStarValue = ((p.Rating / p.Nbr_rater) / 2) - filledStars; // Valeur de la demi-étoile restante

            for (int i = 0; i < 5; i++)
            {
                double starOpacity;

                if (i < filledStars)
                {
                    starOpacity = 1.0; // Étoile entière
                }
                else if (i == filledStars)
                {
                    starOpacity = remainingStarValue; // Demi-étoile (valeur de la demi-étoile restante)
                }
                else
                {
                    starOpacity = 0.18; // Étoile vide
                }

                Image starImage = new Image
                {
                    Source = new BitmapImage(new Uri("../Images/star.png", UriKind.Relative)), 
                    Width = starSize,
                    Height = starSize,
                    Opacity = starOpacity
                };

                starsPanel.Children.Add(starImage);
            }
        }

    }
}
