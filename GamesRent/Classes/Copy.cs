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

    public void DeleteCopy(int id_copy)
    {
        CopyDAO CDAO = new CopyDAO();
        CDAO.DeleteCopy(id_copy);
    }
}
