using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        /*if (!Context.User.Identity.IsAuthenticated)
        {
            MenuItem adminLink = NavigationMenu.FindItem("Admin");
            NavigationMenu.Items.Remove(adminLink);

            MenuItem fixLink = NavigationMenu.FindItem("Products");
            NavigationMenu.Items.Remove(fixLink);

        }
        else if (!Context.User.IsInRole("Administrator"))
        {
            MenuItem adminLink = NavigationMenu.FindItem("Admin");
            NavigationMenu.Items.Remove(adminLink);
        }*/



        if (!IsPostBack)
        {
            populateMenuItem();
        }
    }

    private void populateMenuItem()
    {
        //if (Context.User.Identity.IsAuthenticated)
        //{
            DataTable menuData = GetMenuData();
            AddTopMenuItems(menuData);
            imgDownload.Visible = true;
       // }
         
            
    }

    private DataTable GetMenuData()
    {

        using (SqlCommand cmd = ConnectionManager.Command("GetCategories", ConnectionManager.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
       
    }


    // Filter the data to get only the rows that have a
    /// null ParentID (This will come on the top-level menu items)

    private void AddTopMenuItems(DataTable menuData)
    {
        DataView view = new DataView(menuData);
        view.RowFilter = "CatLevel =0";
        foreach (DataRowView row in view)
        {
            MenuItem newMenuItem = new MenuItem(row["CatName"].ToString(), row["CatId"].ToString(),"",row["Page"].ToString());
            NavigationMenu.Items.Add(newMenuItem);
            AddChildMenuItems(menuData, newMenuItem);
        }

        if (Context.User.IsInRole("Administrator"))
        {
            //MenuItem newMenuItem = new MenuItem("Admin", "~/Admin/Administration.aspx");
            MenuItem newMenuItem = new MenuItem("Admin" , null, null,  "~/Admin/Administration.aspx");
            NavigationMenu.Items.Add(newMenuItem);
        }

    }

    //This code is used to recursively add child menu items by filtering by ParentID

    private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
    {
        DataView view = new DataView(menuData);
        view.RowFilter = "CatLevel=" + parentMenuItem.Value;
        foreach (DataRowView row in view)
        {
            MenuItem newMenuItem = new MenuItem(row["CatName"].ToString(), row["CatId"].ToString());
            parentMenuItem.ChildItems.Add(newMenuItem);
            AddChildMenuItems(menuData, newMenuItem);
        }
    }
      
    protected void imgDownload_Click(object sender, ImageClickEventArgs e)
    {
        Response.ClearHeaders();
        Response.ContentType = "application/zip";
        Response.AddHeader("Content-Disposition", "attachment; filename=RKBrochure.pdf");
        Response.TransmitFile(Server.MapPath("~/Resources/RKBrochure.pdf"));
        Response.End();
    }
}
