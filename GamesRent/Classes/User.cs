using System;
using System.Collections.Generic;

public abstract class User
{
    private int id_user;
    private string username;
    private string password;

    public User(int id_user, string username, string password)
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
