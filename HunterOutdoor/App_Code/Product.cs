using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Fixture
/// </summary>
public class Product
{
    public Product()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public int _productId;
    public string _productName;
    public string _productCategory;
    public string _productFixedSizes;
    public int _productSubCategory;
    public string _description;
    public string _colours;
    public string _size;
    public string _mainImage;
    public string _subImages;
    public string _productImages;

    public int ProductId { get { return _productId; } set { _productId = value; } }
    public string ProductName { get { return _productName; } set { _productName = value; } }
    public string ProductCategory { get { return _productCategory; } set { _productCategory = value; } }
    public string ProductFixedSizes { get { return _productFixedSizes; } set { _productFixedSizes = value; } }

    public int ProductSubCategory { get { return _productSubCategory; } set { _productSubCategory = value; } }
    public string Description { get { return _description; } set { _description = value; } }
    public string Colours { get { return _colours; } set { _colours = value; } }
    public string Size { get { return _size; } set { _size = value; } }
    public string MainImage { get { return _mainImage; } set { _mainImage = value; } }
    public string SubImages { get { return _subImages; } set { _subImages = value; } }
    public string ProductImages { get { return _productImages; } set { _productImages = value; } }



    public int InsertProduct()
    {
        int idcat=0;
        using (SqlCommand cmd = ConnectionManager.Command("InsertProduct", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmProductName", _productName);
            cmd.Parameters.AddWithValue("@prmProductCategory", _productCategory);
            cmd.Parameters.AddWithValue("@prmProductFixedSizes", _productFixedSizes);
            cmd.Parameters.AddWithValue("@prmProductSubCategory", _productSubCategory);
            cmd.Parameters.AddWithValue("@prmDescription", _description);
            cmd.Parameters.AddWithValue("@prmColours", _colours);
            cmd.Parameters.AddWithValue("@prmSize", _size);
            cmd.Parameters.AddWithValue("@prmMainImage", _mainImage);
            cmd.Parameters.AddWithValue("@prmSubImages", _subImages);
            cmd.Parameters.Add("@new_identity", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            idcat = Convert.ToInt32(cmd.Parameters["@new_identity"].Value); 
            cmd.Connection.Close();

            return idcat;

            
        }
    }

    public void UpdateProduct()
    {

        using (SqlCommand cmd = ConnectionManager.Command("UpdateProduct", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmProductId", _productId);
            cmd.Parameters.AddWithValue("@prmProductName", _productName);
            cmd.Parameters.AddWithValue("@prmProductCategory", _productCategory);
            cmd.Parameters.AddWithValue("@prmProductFixedSizes", _productFixedSizes);
            cmd.Parameters.AddWithValue("@prmProductSubCategory", _productSubCategory);
            cmd.Parameters.AddWithValue("@prmDescription", _description);
            cmd.Parameters.AddWithValue("@prmColours", _colours);
            cmd.Parameters.AddWithValue("@prmSize", _size);
            cmd.Parameters.AddWithValue("@prmMainImage", _mainImage);
            cmd.Parameters.AddWithValue("@prmSubImages", _subImages);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        } 
    }

 
    public void GetProduct(int aproductId)
    {
        using (SqlCommand cmd = ConnectionManager.Command("GetProducts", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmProductId", aproductId);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this._productId = Convert.ToInt32(reader["ProductId"]);
                    this._productName = reader["ProductName"].ToString();
                    this._description = reader["Description"].ToString();
                    this._mainImage = reader["MainImage"].ToString();
                    this._subImages = reader["SubImages"].ToString();
                    this._colours = reader["Colours"].ToString();
                    this._size = reader["Size"].ToString();
                    this._productImages = reader["ProductImages"].ToString();
                    this._productSubCategory = Convert.ToInt32(reader["ProductSubCategory"]);
                    this._productCategory = reader["ProductCategory"].ToString();
                    this._productFixedSizes = reader["productFixedSizes"].ToString();
                }
            }

            cmd.Connection.Close();
            reader.Close();
        }
    }


    public void DeleteProductImage(int proId, string imageType)
    {
        using (SqlCommand cmd = ConnectionManager.Command("DeleteProductImage", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmProductId", proId);
            cmd.Parameters.AddWithValue("@prmImageType", imageType);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
     
        }
    }

    public void DeleteProduct(int proId)
    {
        using (SqlCommand cmd = ConnectionManager.Command("DeleteProduct", ConnectionManager.ConnectionString))
        {
            cmd.Parameters.AddWithValue("@prmProductId", proId);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }

}