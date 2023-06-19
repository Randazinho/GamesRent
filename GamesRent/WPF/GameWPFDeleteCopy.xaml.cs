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
    /// Logique d'interaction pour GameWPFDeleteCopy.xaml
    /// </summary>
    public partial class GameWPFDeleteCopy : Window
    {
        public Player p;
        public GameWPFDeleteCopy(int idplayer)
        {
            PlayerDAO Pdao = new PlayerDAO();
            p = Pdao.Find(idplayer);
            InitializeComponent();
        }

        private void Copies_Initialized(object sender, EventArgs e)
        {
            try
            {
                List<Copy> clist = new List<Copy>();
                CopyDAO CDAO = new CopyDAO();
                clist = CDAO.FindAll(clist, p.Id_player);
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
        private void DeleteCopy_Click(object sender, RoutedEventArgs e)
        {
            int idcopy = 0;
            try
            {
                idcopy = Convert.ToInt32(TxtBoxIdCopy.Text);
                if (idcopy > 0)
                {
                    CopyDAO CDAO = new CopyDAO();
                    CDAO.DeleteCopy(idcopy);
                    //recharge la page pour afficher la nouvelle liste
                    GameWPFDeleteCopy dashboard = new GameWPFDeleteCopy(p.Id_player);
                    dashboard.Show();
                    this.Close();
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Error with the Id selected");
                TxtBoxIdCopy.Text = "";
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            GameWPF dashboard = new GameWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    }
}
