using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

public class Admin : User
{
    private int id_admin = 0;
    string date = "";
    public Admin(string date)
    {
        this.date = date;
    }

    public int Id_admin
    {
        get;
        set;
    }
    public override string ToString()
    {
        return "Admin n° : " + id_admin;
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }
}
