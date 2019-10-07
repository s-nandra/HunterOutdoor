using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Products
/// </summary>
public class Products : List<Product>
{
	public Products()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public void GetProducts(int? _catId )
    {
        using (SqlCommand cmd = ConnectionManager.Command("GetProducts", ConnectionManager.ConnectionString))
        {
            cmd.Connection.Open();

            if(_catId!=null)
                cmd.Parameters.AddWithValue("@prmCatId", _catId);
            
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    var cat = new Product();
                    cat.ProductId = Convert.ToInt32(reader["ProductId"]);
                    cat.ProductName = reader["ProductName"].ToString();
                    cat.MainImage = reader["MainImage"].ToString();
                    cat.Description = reader["Description"].ToString();
                    cat.Colours = reader["Colours"].ToString();
                    cat.Size = reader["Size"].ToString();
                    cat.ProductFixedSizes = reader["ProductFixedSizes"].ToString();
                
                    base.Add(cat);
                }
            }

            cmd.Connection.Close();
            reader.Close();
        }
    }
}