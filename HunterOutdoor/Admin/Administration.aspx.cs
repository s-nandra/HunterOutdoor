﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Administration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
             

            //if (User.Identity.Name.Length < 2)
            //    btnSubmit.Visible = false;
        }

    }
     




}
