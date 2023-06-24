using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Loan
{
    private int id_loan = 0;
    private DateTime startDate = new DateTime(2022, 1, 1);
    private DateTime endDate = new DateTime(2022, 1, 1);
    private int ongoing = 0;// Passe à true lorsque la location est en cours et va permettre de calculer l'amende si date dépassée (endDate) et que location toujours en cours
    private Player player;
    private Copy copy;
    public Loan(int id_loan, DateTime startDate, DateTime endDate, int ongoing, Player player, Copy copy)
    {
        this.id_loan = id_loan;
        this.startDate = startDate;
        this.endDate = endDate;
        this.ongoing = ongoing;
        this.player = player;
        this.copy = copy;
    }

    public Loan(int id_loan, DateTime startDate, DateTime endDate, int ongoing)
    {
        this.id_loan = id_loan;
        this.startDate = startDate;
        this.endDate = endDate;
        this.ongoing = ongoing;
    }

    public override string ToString()
    {
        return "|ID : " + id_loan + "| Game Title : " + copy.Game.Name + "| Borrower : " + Player.Pseudo + "| Start Date : " + startDate.ToShortDateString() + "| End Date : " + endDate.ToShortDateString() + "| Ongoing : " + ongoing;
    }
    public string ToStringPlayer()
    {
        return "|ID : " + id_loan + "| Game Title : " + copy.Game.Name + "| Start Date : " + startDate.ToShortDateString() + "| End Date : " + endDate.ToShortDateString() + "| Ongoing : " + ongoing;
    }

    public Loan()
    {

    }
    public int Id_loan
    {
        get;
        set;
    }
    public DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    public DateTime EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }

    public Player Player
    {
        get
        {
            return player;
        }
    }

    public Copy Copy
    {
        get
        {
            return copy;
        }
    }
    public int Ongoing
    {
        get { return ongoing; }
        set { ongoing = value; }
    }

    public static Loan Find(int id_loan)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.Find(id_loan);
    }

    public static List<Loan> FindAllLoan(List<Loan> Loans)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoan(Loans);
    }

    public static List<Loan> FindAllLoanByIdPlayerOngoing(int idplayer, List<Loan> Loans)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoanByIdPlayerOngoing(idplayer,Loans);
    }

    public static List<Loan> FindAllLoanByIdPlayerNotOngoing(int idplayer, List<Loan> Loans)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoanByIdPlayerNotOngoing(idplayer, Loans);
    }

    public static List<Loan> FindAllLoanByIdCopy(List<Loan> Loans, int id_copy)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoanByIdCopy(Loans,id_copy);
    }

    public void CreateLoan(int id_copy, int id_pborrower, int week)
    {
        LoanDAO LDAO = new LoanDAO();
        LDAO.CreateLoan(id_copy,id_pborrower,week);
    }

    public void CalculateBalance(int id_loan)
    {
        LoanDAO LDAO = new LoanDAO();
        LDAO.CalculateBalance(id_loan);
    }
    public void EndLoan(int id_loan)
    {
        LoanDAO LDAO = new LoanDAO();
        LDAO.EndLoan(id_loan);
    }
}
