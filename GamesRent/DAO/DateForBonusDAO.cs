using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class DateForBonusDAO : DAO<DateForBonus>
{
    public override bool Create(DateForBonus obj)
    {
        return false;
    }
    public override bool Delete(DateForBonus obj)
    {
        return false;
    }
    public override bool Update(DateForBonus obj)
    {
        return false;
    }
    public override DateForBonus Find(int id)
    {
        throw new NotImplementedException();
    }
    public DateForBonus GetDate()// add bonus selon anniversaire
    {
        DateForBonus Ndate = new DateForBonus();
        try
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.DateForBonus", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Ndate = new DateForBonus
                        {
                            Date = reader.GetString(0)
                        };
                    }
                }
            }
        }
        catch (SqlException)
        {
            throw new Exception("Une erreur sql s'est produite");
        }
        return Ndate;
    }
    public void UpdateToday(string Today)
    {
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString))
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                String updateGame = "UPDATE dbo.DateForBonus SET Today =@Today";
                SqlCommand sqlupdate = new SqlCommand(updateGame, connection);
                sqlupdate.CommandType = CommandType.Text;
                sqlupdate.Parameters.AddWithValue("@Today", Today);
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
