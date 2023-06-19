using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

public class Admin : User
{
    private int id_admin = 0;
    public Admin()
    {
        //Test commit Randaz
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
}
