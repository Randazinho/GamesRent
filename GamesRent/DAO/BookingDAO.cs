using GamesRent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

public class BookingDAO : DAO<Booking>
{
    public Booking booking;
    public BookingDAO() { }
    public override bool Create(Booking obj)
    {
        return false;
    }
    public override bool Delete(Booking obj)
    {
        return false;
    }
    public override bool Update(Booking obj)
    {
        return false;
    }

    public override Booking Find(int id_booking)
    {
        Booking logbook = new Booking();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Player P = new Player();
            Game G = new Game();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.Booking WHERE Id_booking= @id_booking";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@id_booking", id_booking);
                int idclient = Convert.ToInt32(sql.ExecuteScalar());
                if (idclient > 0)
                {
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking logbook2 = new Booking(reader.GetInt32(0), reader.GetDateTime(1), P.Find(reader.GetInt32(2)), G.Find(reader.GetInt32(3)), reader.GetInt32(4));
                            logbook = logbook2;
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
        return logbook;
    }
    public List<Booking> FindAllBookingByPlayerID(List<Booking> Bookings, int id_player)
    {
        Player P = new Player();
        Game G = new Game();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Booking where Player_id = @id_player", connection);
                cmd.Parameters.AddWithValue("id_player", id_player);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking book = new Booking(reader.GetInt32(0), reader.GetDateTime(1), P.Find(reader.GetInt32(2)), G.Find(reader.GetInt32(3)), reader.GetInt32(4));
                        Bookings.Add(book);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Bookings;
    }
    public List<Booking> FindAllBookingByGameID(List<Booking> Bookings, int id_game)
    {
        Player P = new Player();
        Game G = new Game();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Booking where VideoGame_id = @id_game", connection);
                cmd.Parameters.AddWithValue("id_game", id_game);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking book = new Booking(reader.GetInt32(0), reader.GetDateTime(1), P.Find(reader.GetInt32(2)), G.Find(reader.GetInt32(3)), reader.GetInt32(4));
                        Bookings.Add(book);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Bookings;
    }
    public void Delete(int id_booking)//Annulation d'une réservation càd avant qu'une copie ne soit attribuée au joueur
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String deleteGame = "Delete from dbo.Booking WHERE Id_booking=@id_booking";
                SqlCommand sqldelete = new SqlCommand(deleteGame, connection);
                sqldelete.CommandType = CommandType.Text;
                sqldelete.Parameters.AddWithValue("@id_booking", id_booking);
                sqldelete.ExecuteNonQuery();
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
    public int CreateBookingByIdGame(int id_player, int id_game, int week)
    {
        DateTime today = DateTime.Now;
        //MessageBox.Show("idplayer "+ id_player + "idgame : "+id_game);
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Booking B = new Booking();
            int id_booking = 0;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String addGame = "INSERT INTO dbo.Booking (BookingDate,Player_id,VideoGame_id, Week) VALUES (@today, @id_player, @id_game, @week)";
                SqlCommand sqlinsert = new SqlCommand(addGame, connection);
                sqlinsert.CommandType = CommandType.Text;
                sqlinsert.Parameters.AddWithValue("@today", today);
                sqlinsert.Parameters.AddWithValue("@id_player", id_player);
                sqlinsert.Parameters.AddWithValue("@id_game", id_game);
                sqlinsert.Parameters.AddWithValue("@week", week);
                sqlinsert.ExecuteNonQuery();
                id_booking = B.FindLastId(id_booking);
                MessageBox.Show("Games added to your booking wishlist");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown id");
            }
            finally
            {
                connection.Close();
            }
            return id_booking;
        }
    }
    public int FindLastId(int id_booking)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "Select IDENT_CURRENT('Booking')";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
              
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        id_booking = (Int32)reader.GetDecimal(0);
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
        return id_booking;
    }

    public int FindWeekByIDPlayer(int id_player, int id_game)
    {
        Booking logbook = new Booking();
        int week = 0;
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Player P = new Player();
            Game G = new Game();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.Booking WHERE Player_id= @id_player and VideoGame_id = @idgame";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@id_player", id_player);
                sql.Parameters.AddWithValue("@id_game", id_game);
                int idclient = Convert.ToInt32(sql.ExecuteScalar());
                if (idclient > 0)
                {
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking logbook2 = new Booking(reader.GetInt32(0), reader.GetDateTime(1), P.Find(reader.GetInt32(2)), G.Find(reader.GetInt32(3)), reader.GetInt32(4));
                            logbook = logbook2;
                            week= logbook.Week; 
                        }
                    }
                    connection.Close();
                }
                else
                {
                    return 0;
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
        return week;
    }

    public Booking FindABookByIdGameAndIDPlayer(int id_game,int id_player)
    {
        Booking logbook = new Booking();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Player P = new Player();
            Game G = new Game();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.Booking WHERE Player_id = @id_player and VideoGame_id = @id_game";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@id_player", id_player);
                sql.Parameters.AddWithValue("@id_game", id_game);
                int idclient = Convert.ToInt32(sql.ExecuteScalar());
                if (idclient > 0)
                {
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking logbook2 = new Booking(reader.GetInt32(0), reader.GetDateTime(1), P.Find(reader.GetInt32(2)), G.Find(reader.GetInt32(3)), reader.GetInt32(4));
                            logbook = logbook2;
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
        return logbook;
    }
}
