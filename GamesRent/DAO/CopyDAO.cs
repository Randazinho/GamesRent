using GamesRent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Xml.Linq;

public class CopyDAO : DAO<Copy>
{
    public Copy copy;
    public CopyDAO() { }
    public override bool Create(Copy obj)
    {
        return false;
    }
    public override bool Delete(Copy obj)
    {
        return false;
    }
    public override bool Update(Copy obj)
    {
        return false;
    }
    public override Copy Find(int id)
    {
        Copy logcop = new Copy();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Player P = new Player();
            Game G = new Game();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.[Copy] WHERE Id_copy= @id";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@id", id);
                int idclient = Convert.ToInt32(sql.ExecuteScalar());
                if (idclient > 0)
                {
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //0 = Id copy , 1 = id jeu, 2 = id joueur
                            Copy logcop2 = new Copy(reader.GetInt32(0), G.Find(reader.GetInt32(1)), P.Find(reader.GetInt32(2)), reader.GetString(3));
                            logcop = logcop2;
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
        return logcop;
    }
    public List<Copy> FindAll(List<Copy> Copies, int id_player)
    {
        Player P = new Player();
        Game G = new Game();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.[Copy] where Player_owner_id = @id_player", connection);
                cmd.Parameters.AddWithValue("id_player", id_player);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy cop = new Copy(reader.GetInt32(0), G.Find(reader.GetInt32(1)), P.Find(reader.GetInt32(2)), reader.GetString(3));
                        Copies.Add(cop);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Copies;
    }

    public List<Copy> FindAllCopyByGameID(List<Copy> Copies, int id_game)
    {
        Player P = new Player();
        Game G = new Game();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.[Copy] where VideoGame_id = @id_game", connection);
                cmd.Parameters.AddWithValue("id_game", id_game);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Copy cop = new Copy(reader.GetInt32(0), G.Find(reader.GetInt32(1)), P.Find(reader.GetInt32(2)), reader.GetString(3));
                        Copies.Add(cop);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Copies;
    }

    public int CreateCopy(int id_game, int id_player)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Copy C = new Copy();
            int id_copy = 0;
            string available = "YES";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String addGame = "INSERT INTO dbo.[Copy] (VideoGame_id, Player_owner_id,Available) VALUES (@id_game, @id_player,@available)";
                SqlCommand sqlinsert = new SqlCommand(addGame, connection);
                sqlinsert.CommandType = CommandType.Text;
                sqlinsert.Parameters.AddWithValue("@id_game", id_game);
                sqlinsert.Parameters.AddWithValue("@id_player", id_player);
                sqlinsert.Parameters.AddWithValue("@available", available);
                sqlinsert.ExecuteNonQuery();
                id_copy = C.FindLastId(id_copy);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return id_copy;
        }
    }

    public int FindLastId(int id_copy)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "Select IDENT_CURRENT('Copy')";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;

                using (SqlDataReader reader = sql.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id_copy = (Int32)reader.GetDecimal(0);
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
        return id_copy;
    }

    public void ReleaseCopy(int idcopy)
    {
        string available = "YES";
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.Copy SET Available = @available WHERE Id_copy =@idcopy ";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@available", available);
                sqlupdate.Parameters.AddWithValue("@idcopy", idcopy);
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

    public void Borrow(int idcopy)
    {
        string available = "NO";
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.Copy SET Available = @available WHERE Id_copy =@idcopy ";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@available", available);
                sqlupdate.Parameters.AddWithValue("@idcopy", idcopy);
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

    public void DeleteCopy(int id_copy)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String deleteCopy = "Delete from dbo.[Copy] WHERE Id_copy=@id_copy";
                SqlCommand sqldelete = new SqlCommand(deleteCopy, connection);
                sqldelete.CommandType = CommandType.Text;
                sqldelete.Parameters.AddWithValue("@id_copy", id_copy);
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
}
