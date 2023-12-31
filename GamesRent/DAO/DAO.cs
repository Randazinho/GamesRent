﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public abstract class DAO<T>
{
    protected string connectionString = null;
    public DAO()
    {
        this.connectionString = ConfigurationManager.ConnectionStrings["GamesDB"].ConnectionString;
    }
    public abstract bool Create(T obj);
    public abstract bool Delete(T obj);
    public abstract bool Update(T obj);
    public abstract T Find(int id);
}
