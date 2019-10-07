using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Lookup
/// </summary>
public class Lookup
{
	public Lookup()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string _itemName;
    public int _itemValue;

    public int ItemValue { get { return _itemValue; } set { _itemValue = value; } }
    public string ItemName { get { return _itemName; } set { _itemName = value; } }

    
  
}