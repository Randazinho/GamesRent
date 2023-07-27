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
    /// Logique d'interaction pour LoanWPFInProgress.xaml
    /// </summary>
    public partial class LoanWPFInProgress : Window
    {
        public Player p;
        Game G = new Game();
        Booking B = new Booking();
        Player P = new Player();
        public LoanWPFInProgress(int idplayer)
        {
            Player P = new Player();
            p = P.Find(idplayer);
            InitializeComponent();
            List<Item> Items = new List<Item>();
            List<Loan> llist = new List<Loan>();
            Loan L = new Loan();
            llist = L.FindAllLoanByIdPlayerOngoing(p.Id_player,llist);
            int loan = llist.Count;
            if (loan > 0)
            {
                List<Item> items = new List<Item>();
                for (int i = 0; i < loan; i++)
                {
                    items.Add(new Item()
                    {
                        Name = llist[i].ToStringPlayer(),
                        Id = llist[i].Id_loan
                    });
                }
                lstBooking.ItemsSource = items;
                ReturnGame.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No loan in progress for the moment");
                ReturnGame.Visibility = Visibility.Collapsed;
            }
            DataContext = this;
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            LoanWPF dashboard = new LoanWPF(p.Id_player);
            dashboard.Show();
            this.Close();
        }
        
        private void ReturnGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idloan = Convert.ToInt32(SelectedItem.Id);
                if (idloan > 0)
                {
                    Loan L = new Loan();
                    L.EndLoan(idloan);
                    Loan loan = new Loan();
                    loan = L.Find(idloan);
                    //MessageBox.Show("Ongoing changed");
                    Rating dashboard = new Rating(loan.Copy.Id_copy);//id copy
                    dashboard.Show();
                    L.CalculateBalance(idloan);
                    ReturnGame.Visibility = Visibility.Hidden;
                    lstBooking.Visibility = Visibility.Hidden;
                    try
                    {
                        //check si quelqu'un veut louer ce jeu car copy de nouveau available
                        int id_game = loan.Copy.Game.Id_game;
                        int idplayerborrower = G.SelectBooking(id_game);
                        int idcopy = loan.Copy.Id_copy;
                        if (idplayerborrower != 0)//on peut créer la loan
                        {
                            //Select le nombre week de la booking 
                            int week = B.FindWeekByIDPlayer(idplayerborrower, id_game);
                            L.CreateLoan(idcopy, idplayerborrower, week);
                            //empecher de book plusieurs fois le même jeu par un même player en même temps
                            Booking book = B.FindABookByIdGameAndIDPlayer(id_game, idplayerborrower);
                            B.DeleteBooking(book.Id_booking);
                            P.UpdateWallet(loan.Copy.Player_owner.Id_player, G.Find(id_game).CreditCost * week, "+");
                        }
                    }catch
                    {
                        MessageBox.Show("Error with select booking");
                    }
                }
                else throw new Exception("");
            }
            catch
            {
                MessageBox.Show("Pick a loan");
            }
        }

        public Item SelectedItem { get; set; }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = ((RadioButton)sender).DataContext as Item;
        }
    }
}
