using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_AddCategory : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateLookup();

        }

    }

    private void PopulateLookup()
    {
        ddlLevel.Items.Add(new ListItem("Level 0", "0"));
        ddlLevel.Items.Add(new ListItem("Level 1", "1"));

        var _cat = new Categories();
        _cat.GetCategories();


        foreach (Category t in _cat)
        {
            ListItem li = new ListItem(t.CatName, t.Id.ToString());
            this.cbCat.Items.Add(li);
        }


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var c = new Category();
        c.CatName = txtCategory.Text;
        c.CatLevel = Convert.ToInt32(ddlLevel.SelectedValue);

        string _awd = string.Empty;

        if (ddlLevel.SelectedValue == "1")
        {
            foreach (ListItem cBox in cbCat.Items)
            {
                if (cBox.Selected)
                    _awd += cBox.Value + ",";
            }
        }

        if (_awd != string.Empty)
            c.AllowedOnly = _awd;

        c.InsertCategory();

        lblMessage.Text = "Category Saved " + txtCategory.Text;

        txtCategory.Text = string.Empty;

        
    }



}