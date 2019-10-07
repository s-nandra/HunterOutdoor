using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Spare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Products p = new Products();
            p.GetProducts((int)Enumerations.ProductCat.Kids);  

            ListView_Products.DataSource = p;
            ListView_Products.DataBind();
        }
    }
}