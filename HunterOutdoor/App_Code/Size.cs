using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Size
/// </summary>
public class Size
{
	public Size()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int _sizeId;
    public string _sizeName;
    public string _sizeCode;
 
    public int SizeId { get { return _sizeId; } set { _sizeId = value; } }
    public string SizeName { get { return _sizeName; } set { _sizeName = value; } }
    public string SizeCode { get { return _sizeCode; } set { _sizeCode = value; } }

}