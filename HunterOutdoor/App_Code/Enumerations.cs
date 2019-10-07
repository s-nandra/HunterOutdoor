using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Enumerations
/// </summary>
public class Enumerations
{
	public Enumerations()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Quick map to languages as these are not stored in the databse....
    /// </summary>
    public enum ProductCat
    {
      
        Men = 1,
        Women = 2,
        Accessories = 3,
        Kids = 4,
        Group9 = 5,
        Top = 100
        
    }
}