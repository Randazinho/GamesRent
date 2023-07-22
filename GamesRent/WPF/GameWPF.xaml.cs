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
using static GamesRent.WPF.AdminModifyGame;

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
            List<Item> Items = new List<Item>();
            List<Copy> clist = new List<Copy>();
            Copy C = new Copy();
            clist = C.FindAll(clist, p.Id_player);
            int copy = clist.Count;
            if (copy > 0)
            {
                List<Item> items = new List<Item>();
                for (int i = 0; i < copy; i++)
                {
                    items.Add(new Item()
                    {
                        Name = clist[i].ToStringList(),
                        Id = clist[i].Id_copy
                    });
                }
                lstCopy.ItemsSource = items;
            }
            else
            {
                MessageBox.Show("You have no copy for any game at the moment");
            }
            DataContext = this;
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(p.Id_player);
            dashboard.Show();
            this.Close();
        }

        private void AddCopy_Click(object sender, RoutedEventArgs e)
        {
            GameWPFAddCopy dashboard = new GameWPFAddCopy(p.Id_player);
            dashboard.Show();
            this.Close();
        }
    }
}
