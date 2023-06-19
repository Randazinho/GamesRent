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
    /// Logique d'interaction pour MainForAdmin.xaml
    /// </summary>
    public partial class MainForAdmin : Window
    {
        private UserDAO UserDAOAdmin;
        public Admin Admin;
        public MainForAdmin()
        {
            InitializeComponent();
        }
        
        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {
            AdminLoan adminLoanWindow = new AdminLoan();
            adminLoanWindow.Show();
            this.Close();
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            AdminGame admingamewindow = new AdminGame();
            admingamewindow.Show();
            this.Close();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            AdminUser adminUserWindow = new AdminUser();
            adminUserWindow.Show();
            this.Close();
        }

        private void btnUnlog_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen loginwindow = new LoginScreen();
            loginwindow.Show();
            this.Close();
        }
    }
}
