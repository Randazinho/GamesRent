using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Loan
{
    private int id_loan;
    private DateTime startDate = new DateTime();
    private DateTime endDate = new DateTime();
    private int ongoing;
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
        return "| Game Title : " + copy.Game.Name + "| Borrower : " + Player.Pseudo + "| Start Date : " + startDate.ToShortDateString() + "| End Date : " + endDate.ToShortDateString() + "| Ongoing : " + ongoing;
    }
    public string ToStringPlayer()
    {
        TimeSpan diff = endDate - startDate;
        int day = (int)diff.TotalDays;
        int week = day / 7;
        return "| Game Title : " + copy.Game.Name + "| Start Date : " + startDate.ToShortDateString() + "| End Date : " + endDate.ToShortDateString() + "| Amount : " + ((copy.Game.CreditCost) * week) + " cr" + " | Owner "+ copy.Player_owner.Pseudo.ToUpper();
    }

    public Loan()
    {

    }
    public int Id_loan
    {
        get { return id_loan; }
        set { id_loan = value; }
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

    public Loan Find(int id_loan)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.Find(id_loan);
    }

    public List<Loan> FindAllLoan(List<Loan> Loans)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoan(Loans);
    }

    public List<Loan> FindAllLoanByIdPlayerOngoing(int idplayer, List<Loan> Loans)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoanByIdPlayerOngoing(idplayer,Loans);
    }

    public List<Loan> FindAllLoanByIdPlayerNotOngoing(int idplayer, List<Loan> Loans)
    {
        LoanDAO LDAO = new LoanDAO();
        return LDAO.FindAllLoanByIdPlayerNotOngoing(idplayer, Loans);
    }

    public List<Loan> FindAllLoanByIdCopy(List<Loan> Loans, int id_copy)
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
