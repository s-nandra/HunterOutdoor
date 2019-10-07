using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            //try
            //{
            //    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            //    {
            //        HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            //        FormsAuthenticationTicket authTicket = null;
            //        authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //        var cusr = authTicket.Name.Split('|')[0].ToString();
            //        var cpwd = authTicket.Name.Split('|')[1].ToString();
            //        Login(cpwd, cusr, false);
            //    }
            //}
            //catch { }
        }

    }



    protected void btnLogin_Click(object sender, EventArgs e)
    {

        var pwd = Security.Encrypt(txtPassword.Text, true).ToString();
        var usr = txtEmail.Text;
        Login(pwd, usr, true);
    }

    private void Login(string pwd, string usr, bool setNewCookie)
    {
        bool loggedin = false;

        var userName = string.Empty;
        var roles = string.Empty;


        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["HunterConnectionString"].ToString());
        SqlCommand sqlComm = new SqlCommand("CheckLogin", sqlConn);
        sqlComm.CommandType = CommandType.StoredProcedure;
        sqlComm.Parameters.AddWithValue("@prmEmail", usr);
        sqlComm.Parameters.AddWithValue("@prmPassword", pwd);

        sqlConn.Open();
        SqlDataReader dr = sqlComm.ExecuteReader();

        while (dr.Read())
        {

            if (dr["Email"].ToString() == txtEmail.Text)
            {
                userName = dr["Email"].ToString();
                roles = dr["roles"].ToString();
                loggedin = true;
            }
            else
            {
                Response.Write("Wrong username password");
            }
        }
        dr.Close();
        sqlConn.Close();

        //if (roles == "Administrator")
        //    setNewCookie = false;
        if (loggedin == true)
        {
            //if (setNewCookie == true)
            //{
            int timeout = cbxRememberMe.Checked ? 10080 : 30; // Timeout in minutes, 525600 = 365 days | 10080 week.
            // Create a new ticket used for authentication
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1, // Ticket version
               userName,// + "|" + pwd, // Username associated with ticket
               DateTime.Now, // Date/time issued
               DateTime.Now.AddMinutes(timeout),//DateTime.Now.AddMinutes(30), // Date/time to expire
               true, // "true" for a persistent user cookie
               roles, // User-data, in this case the roles
               FormsAuthentication.FormsCookiePath);// Path cookie valid for

            // Encrypt the cookie using the machine key for secure transport
            /*string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                // FormsAuthentication.FormsCookieName, // Name of auth cookie
               hash); // Hashed ticket

            // Set the cookie's expiration time to the tickets expiration time
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            // Add the cookie to the list for outgoing response
            Response.Cookies.Add(cookie);*/

            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            //}
            // Redirect to requested URL, or homepage if no previous page
            // requested
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (returnUrl == null) returnUrl = "~/default.aspx";

            // Don't call FormsAuthentication.RedirectFromLoginPage since it
            // could
            // replace the authentication ticket (cookie) we just added
            Response.Redirect(returnUrl);
        }
        else
            lblMsg.Text = "Error logging in";
    }



}
