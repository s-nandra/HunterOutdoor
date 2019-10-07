using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mens : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Context.User.Identity.IsAuthenticated)
        //    Response.Redirect("~/default.aspx");
        //else
        //{
            this.lvDataPager1.PageSize = 16;

            if (!Page.IsPostBack)
            {
                BindListView();
            }
       // }
    }   

    private void BindListView()
    {
        Products p = new Products();
        p.GetProducts((int)Enumerations.ProductCat.Men);

        ListView_Products.DataSource = p;
        ListView_Products.DataBind();


        repOrderedList.DataSource = p;
        repOrderedList.DataBind();

    }
    protected void ListView_Products_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        //set current page startindex, max rows and rebind to false
        lvDataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        lvDataPager2.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        //rebind List View
        BindListView();
    }

}