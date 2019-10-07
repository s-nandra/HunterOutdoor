using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Results
/// </summary>
public class Result
{
    public Result()
    {
        //
        // TODO: Add constructor logic here


    }

    public string _email;
    public string _competition;
    public string _fixtureType;
    public int _fixId;
    public int _frId;
    public int _homeScore;
    public int _awayScore;
    public int _homeTeamId;
    public int _awayTeamId;
    public DateTime _fixDate;
    public DateTime _date;

    public int _missedGames;
    public int _gamesPlayed;

    public string _firstname;
    public string _surname;
    public string _tot_score;
    public string _sd_score;

    public string Firstname
    {
        get { return _firstname; }
        set { _firstname = value; }
    }

    public string Surname
    {
        get { return _surname; }
        set { _surname = value; }
    }
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Competition
    {
        get { return _competition; }
        set { _competition = value; }
    }

    public int FRID
    {
        get { return _frId; }
        set { _frId = value; }
    }

    public int FixId
    {
        get { return _fixId; }
        set { _fixId = value; }
    }

    public int HomeScore
    {
        get { return _homeScore; }
        set { _homeScore = value; }
    }

    public int AwayScore
    {
        get { return _awayScore; }
        set { _awayScore = value; }
    }

    public int HomeTeamId
    {
        get { return _homeTeamId; }
        set { _homeTeamId = value; }
    }
    public int AwayTeamId
    {
        get { return _awayTeamId; }
        set { _awayTeamId = value; }
    }
    public DateTime FixDate
    {
        get { return _fixDate; }
        set { _fixDate = value; }
    }

    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }


    public string Tot_Score
    {
        get { return _tot_score; }
        set { _tot_score = value; }
    }

    public string SD_Score
    {
        get { return _sd_score; }
        set { _sd_score = value; }
    }

    public int MissedGames
    {
        get { return _missedGames; }
        set { _missedGames = value; }
    }

    public int GamesPlayed
    {
        get { return _gamesPlayed; }
        set { _gamesPlayed = value; }
    }

    public string FixtureType
    {
        get { return _fixtureType; }
        set { _fixtureType = value; }
    }
   

    public void InsertUserResults()
    {
        
        //SqlCommand cmd = ConnectionManager.Command("InsertUserResults", ConnectionManager.ConnectionString);
        //try
        //{
        //    cmd.Parameters.AddWithValue("@prmEmail", _email);
        //    cmd.Parameters.AddWithValue("@prmFixDate", _fixDate);
        //    cmd.Parameters.AddWithValue("@prmFRID", _frId);
        //    cmd.Parameters.AddWithValue("@prmId", _fixId);
        //    cmd.Parameters.AddWithValue("@prmH_Id", _homeTeamId);
        //    cmd.Parameters.AddWithValue("@prmA_Id ", _awayTeamId);
        //    cmd.Parameters.AddWithValue("@prmH_Score", _homeScore);
        //    cmd.Parameters.AddWithValue("@prmA_Score", _awayScore);
        //    cmd.Parameters.AddWithValue("@prmComp", _competition);
        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();
        //}
        //finally
        //{ cmd.Connection.Close(); }


        using (SqlCommand cmd = ConnectionManager.Command("InsertUserResults", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmEmail", _email);
            cmd.Parameters.AddWithValue("@prmFixDate", _fixDate);
            cmd.Parameters.AddWithValue("@prmFixtureType", _fixtureType);
            cmd.Parameters.AddWithValue("@prmFRID", _frId);
            cmd.Parameters.AddWithValue("@prmId", _fixId);
            cmd.Parameters.AddWithValue("@prmH_Id", _homeTeamId);
            cmd.Parameters.AddWithValue("@prmA_Id ", _awayTeamId);
            cmd.Parameters.AddWithValue("@prmH_Score", _homeScore);
            cmd.Parameters.AddWithValue("@prmA_Score", _awayScore);
            cmd.Parameters.AddWithValue("@prmComp", _competition);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }

    public void UpdateFixtureResults()
    {
        SqlCommand cmd = ConnectionManager.Command("UpdateFixtureResults", ConnectionManager.ConnectionString);
        //cmd.Parameters.AddWithValue("@prmDate", _fixDate);
        try
        {
            cmd.Parameters.AddWithValue("@prmFRID", _frId);

            cmd.Parameters.AddWithValue("@prmHomeScore", _homeScore);
            cmd.Parameters.AddWithValue("@prmAwayScore", _awayScore);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        { cmd.Connection.Close(); }

    }







}