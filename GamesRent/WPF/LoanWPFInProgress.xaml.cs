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
    public partial class LoanWPFInProgress : Window
    {
        public Player p;
        public LoanWPFInProgress(int idplayer)
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
            Llist = LDAO.FindAllLoanByIdPlayerOngoing(p.Id_player, Llist);
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
                Loans.Content = "No Ongoing Loan at the moment";
            }
        }

        private void ReturnGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idloan = Convert.ToInt32(TxtBoxId.Text);
                if (idloan > 0)
                {
                    LoanDAO LDAO = new LoanDAO();
                    LDAO.EndLoan(idloan);
                    Loan loan = new Loan();
                    loan = LDAO.Find(idloan);
                    MessageBox.Show("Ongoing changed");
                    Rating dashboard = new Rating(loan.Copy.Id_copy);//id copy
                    dashboard.Show();
                    LDAO.CalculateBalance(idloan);
                    MessageBox.Show("The Lender was well noted, this loan now goes in your old loan list..");
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error in the information filled in");
                TxtBoxId.Text = "";
            }
        }
    }
}
