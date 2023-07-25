using GamesRent;
using GamesRent.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

public class GameDAO : DAO<Game>
{
    public Game game;
    public GameDAO() { }
    public override bool Create(Game obj)
    {
        return false;
    }
    public override bool Delete(Game obj)
    {
        return false;
    }
    public override bool Update(Game obj)
    {
        return false;
    }
    public void UpdateCostByID (int id_game, int cr_cost)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.Game SET CreditCost =@cr_cost WHERE Id_game = @id_game";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@id_game", id_game);
                sqlupdate.Parameters.AddWithValue("@cr_cost", cr_cost);
                sqlupdate.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public override Game Find(int id)
    {
        Game logame = new Game();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.Game WHERE Id_game= @id";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@id", id);
                int idclient = Convert.ToInt32(sql.ExecuteScalar());
                if (idclient > 0)
                {
                    using (SqlDataReader readclientinfo = sql.ExecuteReader())
                    {
                        while (readclientinfo.Read())
                        {
                            //0 = Id du jeu , 1 = name , 2 = creditcost, 3 = console , 4 = rating
                            Game logame2 = new Game(readclientinfo.GetInt32(0), readclientinfo.GetString(1), readclientinfo.GetInt32(2), readclientinfo.GetString(3));
                            logame = logame2;
                        }
                    }
                    connection.Close();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return logame;
    }

    public List<Game> FindGameByName(string name,List<Game> Games)
    {
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Game WHERE Name LIKE '%' + @name + '%'", connection);
                cmd.Parameters.AddWithValue("name", name);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game gam = new Game(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                        Games.Add(gam);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Games;
    }

    public List<Game> FindAllGame(List<Game> Games)
    {
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Game", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game gam = new Game(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                        Games.Add(gam);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Games;
    }

    public List<Game> FindGameByConsole(string console, List<Game> Games)
    {
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Game WHERE Console LIKE '%' + @console + '%'", connection);
                cmd.Parameters.AddWithValue("console", console);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game gam = new Game(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                        Games.Add(gam);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Games;
    }

    public List<Game> FindGameByCrCost(int crCost, List<Game> Games)
    {
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Game WHERE CreditCost <= @crCost", connection);
                cmd.Parameters.AddWithValue("crCost", crCost);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Game gam = new Game(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                        Games.Add(gam);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Games;
    }

    public void CreateGameByAdmin(string name, int creditCost, string console)
    {
        float rating = 0;
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String addGame = "INSERT INTO dbo.Game (Name, CreditCost, Console) VALUES (@name, @creditCost, @console)";
                SqlCommand sqlinsert = new SqlCommand(addGame, connection);
                sqlinsert.CommandType = CommandType.Text;
                sqlinsert.Parameters.AddWithValue("@name", name);
                sqlinsert.Parameters.AddWithValue("@creditCost", creditCost);
                sqlinsert.Parameters.AddWithValue("@console", console);
                sqlinsert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public void DeleteGame(int id_game)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String deleteGame = "Delete from dbo.Game WHERE Id_game=@id_game";
                SqlCommand sqldelete = new SqlCommand(deleteGame, connection);
                sqldelete.CommandType = CommandType.Text;
                sqldelete.Parameters.AddWithValue("@id_game", id_game);
                sqldelete.ExecuteNonQuery();
                MessageBox.Show("Game deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("This game is currently being rented..");
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public int CopyAvailable(int id_game, int id_player,int id_booking, int week)// check si copie d'un jeu est dispo
    {
        //MessageBox.Show(" Hello "+id_game+" "+id_player+" "+id_booking);
        List<Loan> Loans = new List<Loan>();
        Loan L = new Loan();
        Booking B = new Booking();
        Game G = new Game();
        int nbrLoan = 0;
        int j = 0;
        int flag1 = 0;
        //check si une copie existe pour ce jeu si oui regarder si elle concernée par une loan si oui est-elle ongoing =1 ?
        List<Copy> clist = new List<Copy>();
        Copy C = new Copy();
        clist = C.FindAllCopyByGameID(clist,id_game);
        int ncopy=clist.Count;
        if(clist.Count > 0)
        {
            while (j < ncopy & flag1 == 0)
            {
                Loans.Clear();
                nbrLoan = 0;
                try
                {
                    Loans = L.FindAllLoanByIdCopy(Loans, clist[j].Id_copy); // Correspond à la copie d'un seul player
                    nbrLoan = Loans.Count;
                    int i = 0, flag2 = 0;
                    while (i < nbrLoan & flag2 == 0)
                    {
                        if (Loans[i].Ongoing == 1)
                        {
                            //une copie existe mais elle est en cours de location
                            flag2 = 1;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    if(flag2==0)
                    {
                        //une copie existe elle a déjà été louée au moins une fois mais elle ne l'est pas pour l'instant => on peut créer la loan
                        Game game = G.Find(id_game);
                        int crcost = game.CreditCost;
                        int player_borrower = id_player;
                        Player P = new Player();
                        Player player = P.Find(id_player);
                        //choper le player_owner 
                        Copy copy =C.Find(clist[j].Id_copy);
                        int id_player_owner = copy.Player_owner.Id_player;
                        //création de la loan
                        if(player.Credit>(game.CreditCost)*week)  
                        {
                            L.CreateLoan(clist[j].Id_copy, id_player, week);
                            flag1 = 1;
                            MessageBox.Show("A Copy is available => Loan created, don't forget to return the game on time to avoid additional costs" +
                                " |Booking deleted");
                            B.DeleteBooking(id_booking);
                            P.UpdateWallet(id_player_owner,crcost*week, "+");
                            //MessageBox.Show("Wallet uptaded");
                            return 1;
                        }
                        else
                        {
                            return 2; // pour refuser de créer la loan car pas assez de cr
                        }
                    }
                    j++;
                }
                catch
                {
                    //une copy du jeu est dispo et n'a jamais été concernée par une loan => on peut créer la loan
                    Game game = G.Find(id_game);
                    int crcost = game.CreditCost;
                    int player_borrower = id_player;
                    Player P = new Player();
                    Player player = P.Find(id_player);
                    //choper le player_owner 
                    Copy copy = C.Find(clist[j].Id_copy);
                    int id_player_owner = copy.Player_owner.Id_player;
                    //création de la loan
                    if (player.Credit > (game.CreditCost) * week)
                    {
                        L.CreateLoan(clist[j].Id_copy, id_player,week);
                        flag1 = 1;
                        MessageBox.Show("A Copy is available =>Loan created, don't forget to return the game on time to avoid additional costs" +
                            " |Booking deleted");
                        B.DeleteBooking(id_booking);
                        P.UpdateWallet(id_player_owner,crcost*week,"+");
                        //MessageBox.Show("Wallet uptaded");
                        return 1;
                    }
                    else
                    {
                        return 2; // pour refuser de créer la loan pas assez de credit
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("No available copy for this game at the moment..");
            return 0;
        }
        MessageBox.Show("No available copy for this game at the moment..");
        return 0;
    }

    public int SelectBooking(int idgame)//sélectionne le joueur à qui on attribue une copie selon les règles de gestion
    {
        Booking B = new Booking();
        List<Booking> bookings = new List<Booking>();
        bookings = B.FindAllBookingByGameID(bookings, idgame);
        int nbr_booker = bookings.Count;
        if (nbr_booker > 0)
        {
            if (nbr_booker == 1)
            {
                return bookings[0].Player.Id_player; //id du seul joueur qui a booké
            }
            else
            {
                //appliquer les règles de gestion sur la liste des booker 
                //1 qui possède le max de crédit ?
                int max = 0, cpt = 0, pos = 0;
                for (int i = 0; i < nbr_booker; i++)
                {
                    if (bookings[i].Player.Credit > max)
                    {
                        max = bookings[i].Player.Credit;
                        pos = i;
                    }
                }
                //compte le nombre de personne qui ont le même maximum
                foreach (Booking booking in bookings)
                {
                    if (booking.Player.Credit == max)
                    {
                        cpt++;
                    }
                }
                if (cpt == 1) // si cpt ==1 un seul booker possède le max de crédit
                {
                    return bookings[pos].Player.Id_player; //on retourne le mec à l'indice pos donc qui avait le max de crédit
                }
                else
                {
                    for (int i = 0; i < nbr_booker; i++)//ici on retire de la liste bookings les éléments qui n'ont pas le max crédit
                    {
                        if (bookings[i].Player.Credit < max)
                        {
                            bookings.Remove(bookings[i]);
                        }
                    }
                    nbr_booker = bookings.Count;
                    //règle 2 : réservation la plus ancienne 
                    cpt = 0;
                    DateTime mindate = new DateTime(2100, 01, 01);
                    for (int i = 0; i < nbr_booker; i++)
                    {
                        if (bookings[i].BookingDate < mindate)
                        {
                            mindate = bookings[i].BookingDate;
                            pos = i;
                        }
                    }
                    //compte le nombre de personne qui ont la même date booking
                    foreach (Booking booking in bookings)
                    {
                        if (booking.BookingDate == mindate)
                        {
                            cpt++;
                        }
                    }
                    if (cpt == 1) // si cpt ==1 un seul booker possède la date la plus ancienne
                    {
                        return bookings[pos].Player.Id_player; //on retourne le mec à l'indice pos donc qui avait la min date
                    }
                    else
                    {
                        for (int i = 0; i < nbr_booker; i++)//ici on retire de la liste bookings les éléments qui sont supérieur à la date booking
                        {
                            if (bookings[i].BookingDate > mindate)
                            {
                                bookings.Remove(bookings[i]);
                            }
                        }
                        nbr_booker = bookings.Count;
                        //règle 3 : Abonné inscrit depuis le plus longtemps 
                        cpt = 0;
                        mindate = new DateTime(2100, 01, 01);
                        for (int i = 0; i < nbr_booker; i++)
                        {
                            if (bookings[i].Player.RegistrationDate < mindate)
                            {
                                mindate = bookings[i].Player.RegistrationDate;
                                pos = i;
                            }
                        }
                        //compte le nombre de personne qui ont la même date booking
                        foreach (Booking booking in bookings)
                        {
                            if (booking.Player.RegistrationDate == mindate)
                            {
                                cpt++;
                            }
                        }
                        if (cpt == 1) // si cpt ==1 un seul booker possède la date la plus ancienne
                        {
                            return bookings[pos].Player.Id_player; //on retourne le mec à l'indice pos donc qui avait la min date
                        }
                        else
                        {
                            for (int i = 0; i < nbr_booker; i++)//ici on retire de la liste bookings les éléments qui ont une date supérieur à la registration date min
                            {
                                if (bookings[i].Player.RegistrationDate > mindate)
                                {
                                    bookings.Remove(bookings[i]);
                                }
                            }
                            nbr_booker = bookings.Count;
                            //règle 4 : Abonné inscrit depuis le plus longtemps 
                            cpt = 0;
                            mindate = new DateTime(2100, 01, 01);
                            for (int i = 0; i < nbr_booker; i++)
                            {
                                if (bookings[i].Player.DateOfBirth < mindate)
                                {
                                    mindate = bookings[i].Player.DateOfBirth;
                                    pos = i;
                                }
                            }
                            //compte le nombre de personne qui ont la même dateofbirth
                            foreach (Booking booking in bookings)
                            {
                                if (booking.Player.DateOfBirth == mindate)
                                {
                                    cpt++;
                                }
                            }
                            if (cpt == 1) // si cpt ==1 un seul booker possède la date la plus ancienne
                            {
                                return bookings[pos].Player.Id_player; //on retourne le mec à l'indice pos donc qui avait la min date
                            }
                            else
                            {
                                for (int i = 0; i < nbr_booker; i++)//ici on retire de la liste bookings les éléments qui ont une date supérieur à la min dateofbirth
                                {
                                    if (bookings[i].Player.DateOfBirth > mindate)
                                    {
                                        bookings.Remove(bookings[i]);
                                    }
                                }
                                nbr_booker = bookings.Count;
                                //règle 5 aléatoire 
                                Random rdn = new Random();
                                int random = rdn.Next(nbr_booker);
                                return bookings[random].Player.Id_player; //on retourne le mec à l'indice random
                            }
                        }
                    }
                }
            }
        }
        else
        {
            return 0;
        }
    }
}

