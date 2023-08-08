using System;
using System.Collections;
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
    /// Logique d'interaction pour LoanWPFOld.xaml
    /// </summary>
    public partial class LoanWPFOld : Window
    {
        public Player p;
        public LoanWPFOld(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            LoadLoans();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            LoanWPF dashboard = new LoanWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void LoadLoans()
        {
            List<Loan> Llist = new List<Loan>();
            Loan L = new Loan();
            Llist = L.FindAllLoanByIdPlayerNotOngoing(p.Id_player, Llist);
            string concats = "";
            if (Llist.Count > 0)
            {
                LoanDataGrid.ItemsSource = Llist;
                LoanDataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                LoanDataGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
