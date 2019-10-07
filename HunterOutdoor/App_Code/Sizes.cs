using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Sizes
/// </summary>
public class Sizes : List<Size>
{
	public Sizes()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public void GetSizes()
    {

        using (SqlCommand cmd = ConnectionManager.Command("GetSizes", ConnectionManager.ConnectionString))
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var s = new Size();
                    s.SizeId = Convert.ToInt32(reader["SizeId"]);
                    s.SizeCode = reader["sizeCode"].ToString();
                    base.Add(s);
                }
            }

            cmd.Connection.Close();
            reader.Close();
        }


    }
}