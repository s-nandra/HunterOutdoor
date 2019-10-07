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
public class Categories : List<Category>
{
    public Categories()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public void GetCategories()
    {
        
        using (SqlCommand cmd = ConnectionManager.Command("GetCategories", ConnectionManager.ConnectionString))
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    var cat = new Category();
                    cat.Id = Convert.ToInt32(reader["CatId"]);
                    cat.CatName = reader["CatName"].ToString();
                    base.Add(cat);
                }
            }

            cmd.Connection.Close();
            reader.Close();
        }

        
    }


    public void GetCategories(int catLevel)
    {

        using (SqlCommand cmd = ConnectionManager.Command("GetCategories", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmCatLevel", catLevel);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    var cat = new Category();
                    cat.Id = Convert.ToInt32(reader["CatId"]);
                    cat.CatName = reader["CatName"].ToString();
                    base.Add(cat);
                }
            }

            cmd.Connection.Close();
            reader.Close();
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

