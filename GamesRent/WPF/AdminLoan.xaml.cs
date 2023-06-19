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
    /// Logique d'interaction pour AdminLoan.xaml
    /// </summary>
    public partial class AdminLoan : Window
    {
        public AdminLoan()
        {
            InitializeComponent();
        }
        private void AdminMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainForAdmin dashboard = new MainForAdmin();
            dashboard.Show();
            this.Close();
        }
        private void Loans_Initialized(object sender, EventArgs e)
        {
            try
            {
                List<Loan> loanlist = new List<Loan>();
                LoanDAO LDAO = new LoanDAO();
                loanlist = LDAO.FindAllLoan(loanlist);
                string concats = "";
                foreach (Loan l in loanlist)
                {
                    concats += l.ToString() + "\n";
                }
                Loans.Content = concats.Substring(0, concats.Length - 1);
            }
            catch
            {
                Loans.Content = "No loan for the moment";
            }
        }
    }
}
