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
    /// Logique d'interaction pour Loan.xaml
    /// </summary>
    public partial class LoanWPF : Window
    {
        public Player p;
        public LoanWPF(int idplayer)
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

        private void InProgress_Click(object sender, RoutedEventArgs e)
        {
            LoanWPFInProgress dashboard = new LoanWPFInProgress(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void OldLoan_Click(object sender, RoutedEventArgs e)
        {
            LoanWPFOld dashboard = new LoanWPFOld(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    }
}
