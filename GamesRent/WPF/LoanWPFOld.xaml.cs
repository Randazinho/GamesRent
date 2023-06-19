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
    /// Logique d'interaction pour LoanWPFOld.xaml
    /// </summary>
    public partial class LoanWPFOld : Window
    {
        public Player p;
        public LoanWPFOld(int idplayer)
        {
            PlayerDAO Pdao = new PlayerDAO();
            p = Pdao.Find(idplayer);
            InitializeComponent();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            LoanWPF dashboard = new LoanWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void Loans_Initialized(object sender, EventArgs e)
        {
            List<Loan> Llist = new List<Loan>();
            LoanDAO LDAO = new LoanDAO();
            Llist = LDAO.FindAllLoanByIdPlayerNotOngoing(p.Id_player, Llist);
            string concats = "";
            if (Llist.Count > 0)
            {
                foreach (Loan l in Llist)
                {
                    concats += l.ToStringPlayer() + "\n";
                }
                Loans.Content = concats.Substring(0, concats.Length - 1);
            }
            else
            {
                Loans.Content = "No Old Loan to show";
            }
        }
    }
}
