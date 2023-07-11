using GamesRent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows;

public class PlayerDAO : DAO<Player>
{
    public Player player;
    public PlayerDAO() { }
    public override bool Create(Player obj)
    {
        return false;
    }
    public override bool Delete(Player obj)
    {
        return false;
    }
    public override bool Update(Player obj)
    {
        return false;
    }
    public override Player Find(int id)
    {
        Player logplayer = new Player();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.Player WHERE Id_player= @id";
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
                            //0 = Id du player , 1 = credit, 2 = Pseudo, 3 = registration date , 4 = annif
                            Player logplay2 = new Player(readclientinfo.GetInt32(0), readclientinfo.GetInt32(1), readclientinfo.GetString(2), readclientinfo.GetDateTime(3), readclientinfo.GetDateTime(4), readclientinfo.GetDouble(6), readclientinfo.GetInt32(7));
                            logplayer = logplay2;
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

        return logplayer;
    }

    public List<Player> FindAllPlayer(List<Player> Players)
    {
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Player", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       Player plr = new Player(reader.GetInt32(0),reader.GetInt32(1), reader.GetString(2),reader.GetDateTime(3),reader.GetDateTime(4), reader.GetDouble(6), reader.GetInt32(7));
                       Players.Add(plr);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Players;
    }
    public int LoanAllowed(int idplayer, int id_game, int week)//Permet d'accorder la location d'un jeu selon son wallet et si booking pas déjà fait sur le même jeu
    {
        Player P = new Player();
        Player player = P.Find(idplayer);
        Game G = new Game();
        Game game = G.Find(id_game);
        BookingDAO BDAO = new BookingDAO();
        Booking booking = BDAO.FindABookByIdGameAndIDPlayer(id_game,idplayer);
        if (player.Credit > game.CreditCost*week & booking==null)
        {
            return 1;
        }
        else if (booking != null)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public void AddBirthDayBonus(String today)// add bonus selon anniversaire
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.Player SET Credit = Credit+2 WHERE concat(Day(DateOfBirth),'-',Month(DateOfBirth)) =@Today ";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@Today", today);
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

    public void UpdateWalletByID(int id_borrower, int ammount ,int id_owner)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateWallet = "UPDATE dbo.Player SET Credit =Credit-@ammount WHERE Id_player = @id_borrower "; 
                SqlCommand sqlupdate = new SqlCommand(updateWallet, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@id_borrower", id_borrower);
                sqlupdate.Parameters.AddWithValue("@ammount", ammount);
                sqlupdate.ExecuteNonQuery();
                String updateWallet2 = "UPDATE dbo.Player SET Credit =Credit+@ammount WHERE Id_player = @id_owner ";
                SqlCommand sqlupdate2 = new SqlCommand(updateWallet2, connection);
                sqlupdate2.CommandType = CommandType.Text;
                sqlupdate2.Parameters.AddWithValue("@id_owner", id_owner);
                sqlupdate2.Parameters.AddWithValue("@ammount", ammount);
                sqlupdate2.ExecuteNonQuery();
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
    public int FindLastId()
    {
        int id_user = 0;
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {  
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "Select IDENT_CURRENT('[User]')";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;

                using (SqlDataReader reader = sql.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id_user = (Int32)reader.GetDecimal(0);
                    }
                }
                connection.Close();

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
        return id_user;
    }
    public void RatingPlayer(int id_player, int note)//maj le rating d'un joueur 
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateWallet = "UPDATE dbo.Player SET Rating = Rating+@note, NbrRater= NbrRater+1 WHERE Id_player = @id_player ";
                SqlCommand sqlupdate = new SqlCommand(updateWallet, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@note", note);
                sqlupdate.Parameters.AddWithValue("@id_player", id_player);
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
}

