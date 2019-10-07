using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ConnectionManager
/// </summary>
public class ConnectionManager
{
 
    public static string ConnectionString
    {
        get { return ConfigurationManager.ConnectionStrings["HunterConnectionString"].ConnectionString; }
    }



    static SqlConnection GetDbConnection(string connString)
    {
        return new SqlConnection(connString);
    }

    public static SqlCommand Command(string procedureName, string connectionString)
    {
        SqlCommand command = new SqlCommand(procedureName);
        command.Connection = GetDbConnection(connectionString);
        command.CommandType = CommandType.StoredProcedure;
        return command;
    }
}