using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Copy : Game //copy hérite de jeu car une copy est comme une instance de jeu
{
    private int id_copy = 0;
    private Game game;
    private Player player;
    public Copy (int id_copy,Game game,Player player)
    {
        this.id_copy = id_copy;
        this.Game = game;
        this.player = player;
    }

    public override string ToString()
    {
        return "ID: "+id_copy+" Copy of  : " + Game.Name +" On "+Game.Console +"| Owned by : "+Player_owner.Pseudo;
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

}
