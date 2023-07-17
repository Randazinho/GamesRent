using GamesRent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public class UserDAO : DAO<User>
{
    public User user;
    public UserDAO() { }
    public override bool Create(User obj)
    {
        return false;
    }
    public override bool Delete(User obj)
    {
        return false;
    }
    public override bool Update(User obj)
    {
        return false;
    }
    public override User Find(int id)
    {
        throw new NotImplementedException();
    }
   
    public User FindUserByUsername(string username)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.[User] WHERE Username=@username";
                SqlCommand sql = new SqlCommand(queryretrieveinfo, connection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@username", username);
                int idclient = Convert.ToInt32(sql.ExecuteScalar());
                if (idclient > 0)
                {
                    using (SqlDataReader readclientinfo = sql.ExecuteReader())
                    {
                        while (readclientinfo.Read())
                        {
                            //0 = Id du jeu , 1 = name , 2 = creditcost, 3 = console , 4 = rating
                            User user2 = new User(readclientinfo.GetInt32(0), readclientinfo.GetString(1), readclientinfo.GetString(2));
                            user = user2;
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
        return user;
    }

    public int Login(string login, string password)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                //MessageBox.Show("Connection.. avec " + login + " " + password);
                String querylogin = "select * from dbo.[User] where Username LIKE @username AND Password LIKE @password";
                SqlCommand sqlcmdlogin = new SqlCommand(querylogin, connection);
                sqlcmdlogin.CommandType = CommandType.Text;
                sqlcmdlogin.Parameters.AddWithValue("@username", login);
                sqlcmdlogin.Parameters.AddWithValue("@password", password);
                int iduser = Convert.ToInt32(sqlcmdlogin.ExecuteScalar());
                if (iduser > 0)
                {
                    using (SqlDataReader reader = sqlcmdlogin.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return iduser;
                        }
                    }
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
        return 0;
    }

    public void CreateNewUser(string username, string password, DateTime dateofbirth)
    {
        int id_player = 0;
        int credit = 10;
        DateTime registrationDate = DateTime.Now;
        if(registrationDate.Month==dateofbirth.Month & registrationDate.Day==dateofbirth.Day)
        {
            credit = credit + 2;
        }
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            PlayerDAO PDAO = new PlayerDAO();
            float rating = 0;
            int nbr_rater = 0;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String addGame = "INSERT INTO dbo.[User] (Username, Password ) VALUES (@username, @password)";
                SqlCommand sqlinsert = new SqlCommand(addGame, connection);
                sqlinsert.CommandType = CommandType.Text;
                sqlinsert.Parameters.AddWithValue("@username", username);
                sqlinsert.Parameters.AddWithValue("@password", password);
                sqlinsert.ExecuteNonQuery();
                id_player = PDAO.FindLastId();
                String addPlayer = "INSERT INTO dbo.Player (Id_player, Credit, Pseudo, RegistrationDate, DateOfBirth, User_id, Rating, NbrRater) VALUES (@id_player, @credit, @pseudo, @registrationDate, @dateofbirth, @user_id, @rating, @nbr_rater)";
                SqlCommand sqlinsert2 = new SqlCommand(addPlayer, connection);
                sqlinsert2.CommandType = CommandType.Text;
                sqlinsert2.Parameters.AddWithValue("@id_player", id_player);
                sqlinsert2.Parameters.AddWithValue("@credit", credit);
                sqlinsert2.Parameters.AddWithValue("@pseudo", username);
                sqlinsert2.Parameters.AddWithValue("@registrationDate", registrationDate);
                sqlinsert2.Parameters.AddWithValue("@dateofbirth", dateofbirth);
                sqlinsert2.Parameters.AddWithValue("@user_id", id_player);
                sqlinsert2.Parameters.AddWithValue("@rating", rating);
                sqlinsert2.Parameters.AddWithValue("@nbr_rater", nbr_rater);
                sqlinsert2.ExecuteNonQuery();
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

    public void UpdateToday(string today)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.Admin SET today =@today";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@today", today);
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
    public string GetDate()// add bonus selon anniversaire
    {
        string ndate="";
        try
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Admin", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        {
                            ndate = reader.GetString(2);
                        };
                    }
                }
            }
        }
        catch (SqlException)
        {
            throw new Exception("Une erreur sql s'est produite");
        }
        return ndate;
    }
}

