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
}
