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
    /// Logique d'interaction pour AdminUser.xaml
    /// </summary>
    public partial class AdminUser : Window
    {
        public AdminUser()
        {
            InitializeComponent();
        }
        private void AdminMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainForAdmin dashboard = new MainForAdmin();
            dashboard.Show();
            this.Close();
        }

        private void Players_Initialized(object sender, EventArgs e)
        {
            List<Player> plrlist = new List<Player>();
            PlayerDAO PDAO = new PlayerDAO();
            plrlist = PDAO.FindAllPlayer(plrlist);
            string concats = "";
            foreach (Player p in plrlist)
            {
                concats += p.ToString() + "\n";
            }
            Players.Content = concats.Substring(0, concats.Length-1);
        }
    }
}
