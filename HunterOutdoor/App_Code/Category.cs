using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Fixture
/// </summary>
public class Category
{
    public Category()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public int _id;
    public string _catName;
    public string _allowedOnly;
    public int _catLevel;
    public int Id { get { return _id; } set { _id = value; } }
    public string CatName { get { return _catName; } set { _catName = value; } }
    public string AllowedOnly { get { return _allowedOnly; } set { _allowedOnly = value; } }
    public int CatLevel { get { return _catLevel; } set { _catLevel = value; } }
        
    
    
    public void InsertCategory()
    {
        using (SqlCommand cmd = ConnectionManager.Command("InsertCategory", ConnectionManager.ConnectionString))
        {
             
            cmd.Parameters.AddWithValue("@prmCatName", _catName);
            cmd.Parameters.AddWithValue("@prmCatLevel", _catLevel);
            
            if(_allowedOnly!=null)
                cmd.Parameters.AddWithValue("@prmAllowedOnly", _allowedOnly);
       
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}