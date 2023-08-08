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
            LoadLoans();
        }
        private void AdminMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainForAdmin dashboard = new MainForAdmin();
            dashboard.Show();
            this.Close();
        }
        private void LoadLoans()
        {
            List<Loan> loanlist = new List<Loan>();
            Loan L = new Loan();
            loanlist = L.FindAllLoan(loanlist);
            if(loanlist.Count == 0)
            {
                LoanDataGrid.Visibility = Visibility.Collapsed;
                MessageBox.Show("No Loan to show");
            }
            else
            {
                LoanDataGrid.ItemsSource = loanlist;
            }
        }
    }
}
