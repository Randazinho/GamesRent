using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Booking
{
    private int id_booking;
    private DateTime bookingDate = new DateTime(); //date à laquelle le réservation est posée par un joueur
    private Player player = new Player();
    private Game game = new Game(); 
    private int week; //pour savoir le nombre de semaine

    public Booking (int id_booking, DateTime bookingDate, Player player, Game game, int week)
    {
        this.id_booking = id_booking;
        this.bookingDate = bookingDate;
        this.player = player;
        this.game = game;
        this.week = week;
    }

    public override string ToString()
    {
        return "| Date : " + bookingDate.ToShortDateString()+"| Game : "+game.Name+"| Console : "+game.Console + " for a duration of : "+week+" week(s)";
    }
    
    public Booking ()
    {

    }
    public int Id_booking
    {
        get { return id_booking; }
        set { id_booking = value; }
    }
    public DateTime BookingDate
    {
        get { return bookingDate; }
        set { bookingDate = value; }
    }

    public Player Player
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
    public int Week 
    {
        get { return week; }
        set { week = value; }
    }
    public Booking Find(int id_booking)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.Find(id_booking);
    }
    public List<Booking> FindAllBookingByPlayerID(List<Booking> Bookings, int id_player)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.FindAllBookingByPlayerID(Bookings, id_player);
    }

    public List<Booking> FindAllBookingByGameID(List<Booking> Bookings, int id_game)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.FindAllBookingByGameID(Bookings, id_game);
    }

    public void DeleteBooking(int id_booking)
    {
        BookingDAO BookDAO = new BookingDAO();
        BookDAO.Delete(id_booking);
    }

    public int CreateBookingByIdGame(int id_player, int id_game, int week)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.CreateBookingByIdGame(id_player, id_game, week);
    }

    public int FindLastId(int id_booking)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.FindLastId(id_booking);
    }

    public int FindWeekByIDPlayer(int id_player, int id_game)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.FindWeekByIDPlayer(id_player, id_game);
    }

    public Booking FindABookByIdGameAndIDPlayer(int id_game, int id_player)
    {
        BookingDAO BookDAO = new BookingDAO();
        return BookDAO.FindABookByIdGameAndIDPlayer(id_game, id_player);
    }

}
