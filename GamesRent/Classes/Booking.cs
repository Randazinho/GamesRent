﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Booking
{
    private int id_booking =0;
    private DateTime bookingDate = new DateTime(2022, 1, 1); //date à laquelle le réservation est posée par un joueur
    private Player player = new Player();
    private Game game = new Game(); //parce le mec veut un certain jeu ballec de savoir quelle copie il va recevoir
    private int week = 0; //pour savoir le nombre de semaine

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
        return "ID Booking : " + id_booking + "| Date : " + bookingDate.ToShortDateString()+"| Game : "+game.Name+"| Console : "+game.Console + " for a duration of : "+week+" week(s)";
    }
    
    public Booking ()
    {

    }
    public int Id_booking
    {
        get;
        set;
    }
    public DateTime BookingDate
    {
        get;
        set;
    }

    public Player Player
    {
        get;
        set;
    }

    public Game Game
    {
        get;
        set;
    }
    public int Week 
    { 
      get; 
      set; 
    }

}