using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Users
/// </summary>
public class Users : List<User>
{
	public Users()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public void RetrieveFromDatabase()
    {
        //SqlDataReader rdr = null;
        SqlCommand sqlComm = ConnectionManager.Command("GetUsers", ConnectionManager.ConnectionString);
        sqlComm.Connection.Open();
        SqlDataReader rdr = sqlComm.ExecuteReader(); 
        try
        {
            
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    var user = new User();
                    user.UserId = Convert.ToInt32(rdr["UserId"]);
                    user.Firstname = rdr["Firstname"].ToString();
                    user.Surname = rdr["Surname"].ToString();
                    user.Paid = Convert.ToBoolean(rdr["Paid"].ToString());
                    base.Add(user);
                }
            }
        }
        finally
        {
            rdr.Dispose();
            sqlComm.Connection.Close();
        }
    }

    //public void RetrieveFromDatabase1()
    //{
    //    SqlDataReader rdr = null;
    //    SqlConnection sqlConn = null;

    //    sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["epl_1314ConnectionString"].ToString());
    //    SqlCommand sqlComm = new SqlCommand("GetUsers", sqlConn);
    //    sqlComm.CommandType = CommandType.StoredProcedure;
    //    //sqlComm.Parameters.AddWithValue("@clubid", clubId);
    //    //SqlParameter returnValue = new SqlParameter("returnValue", SqlDbType.Int);
    //    //returnValue.Direction = ParameterDirection.ReturnValue;
    //    //sqlComm.Parameters.Add(returnValue);
    //    sqlConn.Open();
    //    rdr = sqlComm.ExecuteReader();
    //    if (rdr.HasRows)
    //    {
    //        while (rdr.Read())
    //        {
    //            var user = new User();
    //            user.UserId = Convert.ToInt32(rdr["UserId"]);
    //            user.Firstname = rdr["Firstname"].ToString();
    //            user.Surname = rdr["Surname"].ToString();
    //            base.Add(user);        
    //        }
    //    }
    //    rdr.Dispose();
    //    sqlConn.Close();
    //}

    
        
 
  
}

