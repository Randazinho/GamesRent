using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

public class Player : User
{
    private int id_player;
    private int credit;
    private string pseudo;
    private DateTime registrationDate = new DateTime();
    private DateTime dateOfBirth = new DateTime();
    private List<Copy> copys = new List<Copy>();
    private List<Booking> bookings = new List<Booking>();
    private List<Loan> loans = new List<Loan>();
    private double rating;
    private int nbr_rater;
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
        return "->" + " |Pseudo :  " + pseudo + " |BirthDay :  " + dateOfBirth.ToShortDateString() +
           " |Credit(s) :  " + credit + " |Registration Date :  " + registrationDate.ToShortDateString() +
           $" |Rating : {rating / nbr_rater:F1}/10";
    }

    public string ToStringAboutMe()
    {
        return
            "\tYou have been registered since "+registrationDate.ToShortDateString()
            + "\n\tYour birthday is " + dateOfBirth.ToShortDateString()
            + $"\n\tYour reputation is {rating / nbr_rater:F1}/10 according to {nbr_rater} rating(s)\t";
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

    public void UpdateWalletForBooking(int id_player, int ammount, string operateur)
    {
        PlayerDAO PDAO = new PlayerDAO();
        PDAO.UpdateWalletForBooking(id_player, ammount, operateur);
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
