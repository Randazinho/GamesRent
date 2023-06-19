using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class DateForBonus
{
    string date = "";
    public DateForBonus()
    {
    }
    public DateForBonus(string date)
    {
        this.date = date;
    }
    public string Date
    {
        get { return date; }
        set { date = value; }
    }
}
