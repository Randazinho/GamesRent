using GamesRent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows;

public class LoanDAO : DAO<Loan>
{
    public Loan loan;
    public LoanDAO() { }
    public override bool Create(Loan obj)
    {
        return false;
    }
    public override bool Delete(Loan obj)
    {
        return false;
    }
    public override bool Update(Loan obj)
    {
        return false;
    }
    public override Loan Find(int id)
    {
        Loan loan = new Loan();
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            Player P = new Player();
            Copy C = new Copy();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String queryretrieveinfo = "SELECT * FROM dbo.Loan WHERE Id_loan = @id";
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
                            //Id loan, startdate, endate, ongoing, playerid, copyid
                            Loan lloan2 = new Loan(readclientinfo.GetInt32(0), readclientinfo.GetDateTime(1), readclientinfo.GetDateTime(2), readclientinfo.GetInt32(3), P.Find(readclientinfo.GetInt32(4)), C.Find(readclientinfo.GetInt32(5)));
                            loan = lloan2;
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
        return loan;
    }

    public List<Loan> FindAllLoan(List<Loan> Loans)
    {
        Player P = new Player();
        Copy C = new Copy();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Loan", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan lo = new Loan(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetInt32(3), P.Find(reader.GetInt32(4)), C.Find(reader.GetInt32(5)));
                        Loans.Add(lo);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Loans;
    }

    public List<Loan> FindAllLoanByIdPlayerOngoing(int idplayer, List<Loan> Loans)
    {
        Player P = new Player();
        Copy C = new Copy();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Loan WHERE Player_borrower_id = @idplayer and Ongoing=1", connection);
                cmd.Parameters.AddWithValue("@idplayer", idplayer);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan lo = new Loan(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetInt32(3), P.Find(reader.GetInt32(4)), C.Find(reader.GetInt32(5)));
                        Loans.Add(lo);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Loans;
    }

    public List<Loan> FindAllLoanByIdPlayerNotOngoing(int idplayer, List<Loan> Loans)
    {
        Player P = new Player();
        Copy C = new Copy();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Loan WHERE Player_borrower_id = @idplayer and Ongoing=0", connection);
                cmd.Parameters.AddWithValue("@idplayer", idplayer);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan lo = new Loan(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetInt32(3), P.Find(reader.GetInt32(4)), C.Find(reader.GetInt32(5)));
                        Loans.Add(lo);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Loans;
    }

    public List<Loan> FindAllLoanByIdCopy(List<Loan> Loans, int id_copy)
    {
        Player P = new Player();
        Copy C = new Copy();
        using (SqlConnection connection = new SqlConnection(this.connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.Loan where Copy_id = @id_copy", connection);
                cmd.Parameters.AddWithValue("id_copy", id_copy);
                connection.Open();
                using (SqlDataReader readclientinfo = cmd.ExecuteReader())
                {
                    while (readclientinfo.Read())
                    {
                        Loan loa = new Loan(readclientinfo.GetInt32(0), readclientinfo.GetDateTime(1), readclientinfo.GetDateTime(2), readclientinfo.GetInt32(3), P.Find(readclientinfo.GetInt32(4)), C.Find(readclientinfo.GetInt32(5)));
                        Loans.Add(loa);
                    }
                }
            }
            catch (SqlException)
            {
                throw new System.Exception("Une erreur sql s'est produite!");
            }
            connection.Close();
        }
        return Loans;
    }

    public void CreateLoan(int id_copy, int id_pborrower, int week)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            DateTime startDate = DateTime.Now;
            week = week * 7; //pour avoir le nombre de jour
            DateTime endDate = startDate.AddDays(week);
            int ongoing = 1;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String addGame = "INSERT INTO dbo.Loan (StartDate, EndDate, Ongoing, Player_borrower_id, Copy_id) VALUES (@startDate, @endDate,@ongoing,@id_pborrower,@id_copy)";
                SqlCommand sqlinsert = new SqlCommand(addGame, connection);
                sqlinsert.CommandType = CommandType.Text;
                sqlinsert.Parameters.AddWithValue("@startDate", startDate);
                sqlinsert.Parameters.AddWithValue("@endDate", endDate);
                sqlinsert.Parameters.AddWithValue("@ongoing", ongoing);
                sqlinsert.Parameters.AddWithValue("@id_pborrower", id_pborrower);
                sqlinsert.Parameters.AddWithValue("@id_copy", id_copy);
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

    public void CalculateBalance(int id_loan)//calculer le prix d'une amende si retard
    {
        Loan loan = Find(id_loan);
        Player P = new Player();
        Copy C = new Copy();
        int note = 10;
        Copy copy =C.Find(loan.Copy.Id_copy);
        //MessageBox.Show("Copy : "+copy.ToString());
        if (loan.EndDate<DateTime.Now)// il est en retard
        {
            TimeSpan Ts = DateTime.Now - loan.EndDate;
            int nbrjour = (Int32)Ts.TotalDays;
            int amende = nbrjour * 5;
            P.UpdateWalletByID(loan.Player.Id_player, amende,copy.Player_owner.Id_player);
            MessageBox.Show("You have paid the fine of "+amende);
            note = note-nbrjour;
            if(note<0)
            {
                note = 0;
            }
            P.RatingPlayer(loan.Player.Id_player, note);
        }
        else
        {
            MessageBox.Show("Game returned on time, thank you");
            P.RatingPlayer(loan.Player.Id_player, note);
        }
    }
    public void EndLoan(int id_loan)//mettre fin à réservation càd joueur qui rend son jeu => remettre le ongoing à 0
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.Loan SET Ongoing =0  WHERE Id_loan = @id_loan";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@id_loan", id_loan);
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

