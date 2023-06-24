using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Game
{
    private int id_game = 0;
    private string name = "";
    private int creditCost = 0;
    private string console = "";
    private List<Copy> copys = new List<Copy>();

    public Game(int id_game,string name, int creditCost, string console)
    {
        this.id_game = id_game;
        this.name = name;
        this.creditCost = creditCost;
        this.console = console;
    }


    public override string ToString()
    {
        return "ID :" + id_game + " Title : " + name + " Console : " + console + " CreditCost : " + creditCost;
    }

    public Game() {}

    public int Id_game
    {
        get { return id_game; }
        set { id_game = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Console
    {
        get { return console; }
        set { console = value; }
    }
    
    public int CreditCost
    {
        get { return creditCost; }
        set { creditCost = value; }
    }

    public List<Copy> Copys
    {
        get { return copys; }
        set { copys = value; }
    }
    public void AddCopy(Copy copy)
    {
        this.copys.Add(copy);
    }
    public void RemoveGame(Copy copy)
    {
        this.copys.Remove(copy);
    }

    public void UpdateCostByID(int id_game, int cr_cost)
    {
        GameDAO GameDao = new GameDAO();
        GameDao.UpdateCostByID(id_game, cr_cost);
    }

    public static Game Find(int id_game)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.Find(id_game);
    }

    public static List<Game> FindGameByName(string name, List<Game> Games)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.FindGameByName(name, Games);
    }

    public static List<Game> FindAllGame(List<Game> Games)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.FindAllGame(Games);
    }

    public static List<Game> FindGameByConsole(string console, List<Game> Games)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.FindGameByConsole(console,Games);
    }

    public static List<Game> FindGameByCrCost(int crCost, List<Game> Games)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.FindGameByCrCost(crCost, Games);
    }

    public void CreateGameByAdmin(string name, int creditCost, string console)
    {
        GameDAO GameDao = new GameDAO();
        GameDao.CreateGameByAdmin(name, creditCost,console);
    }

    public void DeleteGame(int id_game)
    {
        GameDAO GameDao = new GameDAO();
        GameDao.DeleteGame(id_game);
    }

    public static int CopyAvailable(int id_game, int id_player, int id_booking, int week)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.CopyAvailable(id_game,id_player,id_booking,week);
    }

    public int SelectBooking(int idgame)
    {
        GameDAO GameDao = new GameDAO();
        return GameDao.SelectBooking(id_game);
    }

}
