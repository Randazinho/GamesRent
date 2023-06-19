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
            DateForBonus DateB = new DateForBonus();
            DateForBonusDAO DDAO = new DateForBonusDAO();
            DateB= DDAO.GetDate();
            //MessageBox.Show(""+DateB.Date.ToString()+" "+ DateTime.Now.ToShortDateString());
            if (DateTime.Now.ToShortDateString() != DateB.Date)
            {
                InitializeComponent();
                PlayerDAO PDAO = new PlayerDAO();
                PDAO.AddBirthDayBonus(DateTime.Now.ToString("d-M"));
                DDAO.UpdateToday(DateTime.Now.ToShortDateString());
            }
            else
            {
                InitializeComponent();
            }
        }

        private void btnSubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            UserDAO user = new UserDAO();
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
