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
    /// Logique d'interaction pour SignUP.xaml
    /// </summary>
    public partial class SignUP : Window
    {
        public SignUP()
        {
            InitializeComponent();
        }

        private void btnSubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            User User = new User();
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string date = LabelForbirthday.Text;
            try
            {
                DateTime dateofbirth = DateTime.Parse(date);
                //créer un compte
                //check si le nouveau username existe déjà ?
                User user = User.FindUserByUsername(username);
                if (user == null)
                {
                    User.CreateNewUser(username, password, dateofbirth);
                    MessageBox.Show("Account created");
                    LoginScreen dashboard = new LoginScreen();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This username already exist..");
                    txtUsername.Text = "";
                    txtPassword.Password = "";
                }
            }
            catch
            {
                MessageBox.Show("Please fill in all fields");
            }     
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen dashboard = new LoginScreen();
            dashboard.Show();
            this.Close();
        }
    }
}
