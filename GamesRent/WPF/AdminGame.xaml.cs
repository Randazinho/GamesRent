﻿using System;
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
    /// Logique d'interaction pour AdminGame.xaml
    /// </summary>
    public partial class AdminGame : Window
    {
        public AdminGame()
        {
            InitializeComponent();
            LoadGames();
        }
        private void LoadGames()
        {
            List<Game> glist = new List<Game>();
            Game G = new Game();
            glist = G.FindAllGame(glist);
            GamesDataGrid.ItemsSource = glist;
        }
        private void AdminMainMenu_Click(object sender, RoutedEventArgs e)
        {
            //pas besoin de filer d'id, une fois identifié avec login => un seul admin
            MainForAdmin dashboard = new MainForAdmin();
            dashboard.Show();
            this.Close();
        }
        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            AdminAddGame dashboard = new AdminAddGame();
            dashboard.Show();
            this.Close();
        }
        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            AdminDeleteGame dashboard = new AdminDeleteGame();
            dashboard.Show();
            this.Close();
        }
        private void ModifyGame_Click(object sender, RoutedEventArgs e)
        {
            AdminModifyGame dashboard = new AdminModifyGame();
            dashboard.Show();
            this.Close();
        }
    }
}
