using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
	public User()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int _userId;
    public string _firstname;
    public string _surname;
    public string _email;
    public string _password;
    public bool _paid;

    public int UserId { get { return _userId; } set { _userId = value; } }
    public string Firstname { get { return _firstname; } set { _firstname = value; } }
    public string Surname { get { return _surname; } set { _surname = value; } }
    public string Email { get { return _email; } set { _email = value; } }
    public string Password { get { return _password; } set { _password = value; } }
    public bool Paid { get { return _paid; } set { _paid = value; } }

    public void CreateUser()
    {
        using (SqlCommand cmd = ConnectionManager.Command("CreateUser", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmFirstname", _firstname);
            cmd.Parameters.AddWithValue("@prmSurname", _surname);
            cmd.Parameters.AddWithValue("@prmEmail", _email);
            
            cmd.Parameters.AddWithValue("@prmPassword", _password); 
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }

}