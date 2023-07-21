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
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class GameWPF : Window
    {
        public Player p;
        public GameWPF(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void Copies_Initialized(object sender, EventArgs e)
        {
            try
            {
                List<Copy> clist = new List<Copy>();
                Copy C = new Copy();
                clist = C.FindAll(clist, p.Id_player);
                string concats = "";
                foreach (Copy c in clist)
                {
                    concats += c.ToString() + "\n";
                }
                Copies.Content = concats.Substring(0, concats.Length - 1);
            }
            catch
            {
                MessageBox.Show("You have no copy for any game at the moment");
                Copies.Content = "No copy to show";
            }
        }

        private void AddCopy_Click(object sender, RoutedEventArgs e)
        {
            GameWPFAddCopy dashboard = new GameWPFAddCopy(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    }
}
