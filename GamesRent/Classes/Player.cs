using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

public class Player : User
{
    private int id_player = 0;
    private int credit = 0;
    private string pseudo = "";
    private DateTime registrationDate = new DateTime(2022, 1, 1);
    private DateTime dateOfBirth = new DateTime(2022, 1, 1);
    private List<Copy> copys = new List<Copy>();
    private List<Booking> bookings = new List<Booking>();
    private List<Loan> loans = new List<Loan>();
    private double rating =0;
    private int nbr_rater =0;
    public Player(int id_player,int credit, string pseudo, DateTime registrationDate, DateTime dateOfBirth, double rating, int nbr_rater)
    {
        this.id_player = id_player;
        this.credit = credit;
        this.pseudo = pseudo;
        this.registrationDate = registrationDate;
        this.dateOfBirth = dateOfBirth;
        this.rating = rating;
        this.Nbr_rater = nbr_rater;
    }
    public override string ToString()
    {
        return "-> ID: " + id_player + " |Pseudo :  " + pseudo + " |BirthDay :  " + dateOfBirth.ToShortDateString() + " |Credit(s) :  " + credit + " |Registration Date :  " + registrationDate.ToShortDateString() +"|Rating : "+(rating/nbr_rater)+"/10";
    }
    public Player ()
    {

    }
    public int Id_player
    {
        get { return id_player; }
        set { id_player = value; }
    }
   
    public string Pseudo
    {
        get { return pseudo;}
        set { pseudo = value; }
    }

    public int Credit
    {
        get { return credit; }
        set { credit = value; }
    }

    public DateTime RegistrationDate
    {
        get { return registrationDate; }
        set { registrationDate = value; }
    }

    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set { dateOfBirth = value; }
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
    public List<Booking> Bookings
    {
        get { return bookings; }
        set { bookings = value; }
    }
    public void AddBooking(Booking booking)
    {
        this.bookings.Add(booking);
    }
    public void RemoveBooking(Booking booking)
    {
        this.bookings.Remove(booking);
    }

    public List<Loan> Loans
    {
        get { return loans; }
        set { loans = value; }
    }
    public void AddLoan(Loan loan)
    {
        this.loans.Add(loan);
    }
    public void RemoveLoan(Loan loan)
    {
        this.loans.Remove(loan);
    }

    public double Rating
    {
        get { return rating; }
        set { rating = value; }
    }

    public int Nbr_rater
    {
        get { return nbr_rater; }
        set { nbr_rater = value; }
    }

    public Player Find(int id_player)
    {
        PlayerDAO PDAO = new PlayerDAO();
        return PDAO.Find(id_player);
    }

    public List<Player> FindAllPlayer(List<Player> Players)
    {
        PlayerDAO PDAO = new PlayerDAO();
        return PDAO.FindAllPlayer(Players);
    }

    public int LoanAllowed(int idplayer, int id_game, int week)
    {
        PlayerDAO PDAO = new PlayerDAO();
        return PDAO.LoanAllowed(idplayer,id_game,week);
    }

    public void AddBirthDayBonus(String today)
    {
        PlayerDAO PDAO = new PlayerDAO();
        PDAO.AddBirthDayBonus(today);
    }

    public void UpdateWalletByID(int id_borrower, int ammount, int id_owner)
    {
        PlayerDAO PDAO = new PlayerDAO();
        PDAO.UpdateWalletByID(id_borrower,ammount,id_owner);
    }

    public int FindLastId()
    {
        PlayerDAO PDAO = new PlayerDAO();
        return PDAO.FindLastId();
    }

    public void RatingPlayer(int id_player, int note)
    {
        PlayerDAO PDAO = new PlayerDAO();
        PDAO.RatingPlayer(id_player,note);
    }


}
