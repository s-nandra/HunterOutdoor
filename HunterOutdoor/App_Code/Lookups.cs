using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Lookups
/// </summary>
public class Lookups : List<Lookup>
{
	public Lookups()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void GetTeams()
    {

        using (SqlCommand cmd = ConnectionManager.Command("GetTeams", ConnectionManager.ConnectionString))
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Lookup lk = new Lookup();
                    lk.ItemName = reader["TeamName"].ToString();
                    lk.ItemValue = Convert.ToInt32(reader["TeamNo"]);
                    base.Add(lk);
                }
            }
            reader.Dispose();
            cmd.Connection.Close();
        }
    }
}