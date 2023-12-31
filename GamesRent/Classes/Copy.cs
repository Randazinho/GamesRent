﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Copy : Game //copy hérite de jeu car une copy est comme une instance de jeu
{
    private int id_copy;
    private Game game;
    private Player player;
    private string available;
    public Copy (int id_copy, Game game, Player player, string available)
    {
        this.id_copy = id_copy;
        this.Game = game;
        this.player = player;
        this.available = available;
    }

    public override string ToString()
    {
        return " Copy of  : " + Game.Name +" On "+Game.Console +"| Owned by : "+Player_owner.Pseudo;
    }

    public string ToStringList()
    {
        List<Loan> llist = new List<Loan>();
        Loan L = new Loan();
        llist = L.FindAllLoanByIdCopy(llist, Id_copy);
        int nbr = llist.Count;
        int flag = -1;
        if(nbr>0)
        {
            for(int i=0;i<nbr;i++)
            {
                if (llist[i].Ongoing==1)
                {
                    flag = i;
                }
            }
            if(flag!=-1)
            {
                return " Copy of  : " + Game.Name + " On " + Game.Console + " |Loaned to " + llist[flag].Player.Pseudo.ToUpper() +" since " + llist[flag].StartDate.ToShortDateString();
            }
            else
            {
                return " Copy of  : " + Game.Name + " On " + Game.Console +" | Available : "+available + " | Loaned : " + nbr +" time(s)";
            }
        }
        else
        {
            return " Copy of  : " + Game.Name + " On " + Game.Console +" | Available : "+available;
        }
    }

    public Copy()
    {
    
    }

    public int Id_copy
    {
        get { return id_copy; }
        set { id_copy = value; }
    }
    public Game Game
    {
        get
        {
            return game;
        }
        set
        {
            game = value;
        }
    }

    public Player Player_owner
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    public string Available
    {
        get
        {
            return available;
        }
        set
        {
            available = value;
        }
    }
    public Copy Find(int id_copy)
    {
        CopyDAO CDAO = new CopyDAO();
        return CDAO.Find(id_copy);
    }

    public List<Copy> FindAll(List<Copy> Copies, int id_player)
    {
        CopyDAO CDAO = new CopyDAO();
        return CDAO.FindAll(Copies, id_player);
    }

    public List<Copy> FindAllCopyByGameID(List<Copy> Copies, int id_game)
    {
        CopyDAO CDAO = new CopyDAO();
        return CDAO.FindAllCopyByGameID(Copies, id_game);
    }
    public int CreateCopy(int id_game,int id_player)
    {
        CopyDAO CDAO = new CopyDAO();
        return CDAO.CreateCopy(id_game, id_player);
    }

    public int FindLastId(int id_copy)
    {
        CopyDAO CDAO = new CopyDAO();
        return CDAO.FindLastId(id_copy);
    }

    public void ReleaseCopy(int id_copy)
    {
        CopyDAO CDAO = new CopyDAO();
        CDAO.ReleaseCopy(id_copy);
    }

    public void Borrow(int id_copy)
    {
        CopyDAO CDAO = new CopyDAO();
        CDAO.Borrow(id_copy);
    }

    public void DeleteCopy(int id_copy)
    {
        CopyDAO CDAO = new CopyDAO();
        CDAO.DeleteCopy(id_copy);
    }
}
