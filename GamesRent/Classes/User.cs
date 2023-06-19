﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Reflection.PortableExecutable;

public class User
{
    private int id_user = 0;
    private string username = "";
    private string password = "";
    
    public User(int id_user,string username, string password)
    {
        this.id_user = id_user;
        this.username = username;
        this.password = password;
    }
    public override string ToString()
    {
        return "User : " + username + " Password : " + password;
    }
    public User()
    {

    }

    public string Username
    {
        get;
        set;
    }
    public string Password
    {
        get;
        set;
    }
    public int Id_user
    {
        get;
        set;
    }
}