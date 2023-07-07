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
    /// Logique d'interaction pour LoanWPFInProgress.xaml
    /// </summary>
    public partial class Rating : Window
    {
        public Player p;
        Player P = new Player();
        Copy copy = new Copy();
        public Rating(int idcopy)
        {
            copy = copy.Find(idcopy);
            InitializeComponent();
        }

        private void btnSubRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rate = Convert.ToInt32(TxtBoxRating.Text);
                //MessageBox.Show(" " + rate);
                if(rate>0 && rate<=10)
                {
                    P.RatingPlayer(copy.Player_owner.Id_player, rate);
                    MessageBox.Show("The Lender was well noted, this loan now goes in your old loan list..");
                    this.Close();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Pick a good rating 1->10");
                TxtBoxRating.Text = "";
            }
        }
    }
}
