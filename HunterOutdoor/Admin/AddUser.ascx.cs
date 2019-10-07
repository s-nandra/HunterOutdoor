using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddUser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtFirstname.Text.Length > 2)
        {
            var usr = new User();
            usr.Firstname = txtFirstname.Text;
            usr.Surname = txtSurname.Text;
            usr.Email = txtEmail.Text;
            usr.Password = txtPassword.Text;
            usr.CreateUser();

            lblMessage.Text = "New user added" + txtFirstname.Text + " " + txtSurname.Text;

            txtFirstname.Text = String.Empty;
            txtSurname.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }


    }
}