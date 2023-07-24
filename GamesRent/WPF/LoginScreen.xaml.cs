using GamesRent.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
using static System.Net.Mime.MediaTypeNames;

namespace GamesRent
{
    /// <summary>
    /// Logique d'interaction pour LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private string username = "";
        private string password = "";
        private int iduser;
        private int flag = 0;
        public LoginScreen()
        {
            //On check la var Today dans la BD si elle est égal à DateTime.Now ça veut dire qu'on l'a déjà éxécuté AddBirthdayBonus pour le jour donné
            User U = new User();
            string DateB = U.GetDate();
            if (DateTime.Now.ToShortDateString() != DateB)
            {
                InitializeComponent();
                U.UptadeToday(DateTime.Now.ToShortDateString());
                Player P = new Player();
                P.AddBirthDayBonus(DateTime.Now.ToString("d-M"));
            }
            else
            {
                InitializeComponent();
            }
        }

        private void btnSubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            username = txtUsername.Text;
            password = txtPassword.Password;
            if (username.Equals("") | password.Equals(""))
            {
                MessageBox.Show("Enter Data in both fields");
            }
            else
            {
                iduser = user.Login(username, password);
                if (iduser == 0)
                {
                    MessageBox.Show("Invalid credentials");
                    txtUsername.Text="";
                    txtPassword.Password="";
                }
                else if (iduser == 1)
                {
                    MainForAdmin dashboard = new MainForAdmin();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    int idplayer = iduser;
                    //MessageBox.Show("ID PLAYER :"+iduser);
                    MainWindow dashboard = new MainWindow(idplayer);
                    dashboard.Show();
                    this.Close();
                }
            }
        }

        private void btnSubmitCreate_Click(object sender, RoutedEventArgs e)
        {
            SignUP dashboard = new SignUP();
            dashboard.Show();
            this.Close();
        }
    }
}
