using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Results
/// </summary>
public class Results : List<Result>
{
    public Results()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void GetLeaderBoard()
    {

        using (SqlCommand cmd = ConnectionManager.Command("GetLeaderBoard", ConnectionManager.ConnectionString))
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Result r = new Result();
                    r.Email = reader["Email"].ToString();
                    r.Firstname = reader["Firstname"].ToString();
                    r.Surname = reader["Surname"].ToString();
                    if (reader["Tot_Score"] != null)
                        r.Tot_Score = reader["Tot_Score"].ToString();
                    if (reader["SD_Score"] != null)
                        r.SD_Score = reader["SD_Score"].ToString();

                    r.GamesPlayed = Convert.ToInt32(reader["GamesPlayed"]);
                    r.MissedGames = Convert.ToInt32(reader["MissedGames"]);

                    base.Add(r);
                    //r..PlayerFirstName = reader.GetString(1);
                }
            }
            reader.Dispose();
            cmd.Connection.Close();
        }


    }
}