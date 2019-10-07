using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
//using System.Text.RegularExpressions;

public partial class ProductDetails : System.Web.UI.Page
{

    public int AdId
    {
        get
        {
            if (ViewState["AdId"] != null)
                return (int)ViewState["AdId"];
            else
                return 0;
        }
        set
        {
            ViewState["AdId"] = value;
        }
    }

    public int CurrentPhotoIndex
    {
        get
        {
            if (ViewState["CurrentPhotoIndex"] != null)
                return (int)ViewState["CurrentPhotoIndex"];
            else
                return -1;
        }
        set
        {
            ViewState["CurrentPhotoIndex"] = value;
        }
    }

    public int NumPostBacks
    {
        get
        {
            if (ViewState["NumPostBacks"] != null)
                return (int)ViewState["NumPostBacks"];
            else
            {
                ViewState["NumPostBacks"] = 0;
                return 0;
            }
        }
        set
        {
            ViewState["NumPostBacks"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!Context.User.Identity.IsAuthenticated)
        //    Response.Redirect("~/default.aspx");
        //else
        //{

            // Check if the URL querystring contains a valid ad.
            int adId = 0;
            string adIdQs = Request.QueryString["id"];
            if (adIdQs != null && !Int32.TryParse(adIdQs, out adId))
            {
                Response.Redirect("~/Search.aspx");
            }

            if (!Page.IsPostBack)
            {
                //if (User.Identity.IsAuthenticated)
                //{
                    if (Context.User.IsInRole("Administrator"))
                        lnkEdit.Visible = true;


                    BindDataList();
                //}
            }
        //}
    }

    protected void BindDataList()
    {
        int productId = Convert.ToInt32(Request.QueryString["productId"]);


        Product p = new Product();
        p.GetProduct(productId);

        lblProdTitle.Text = p.ProductName;

        Regex regex = new Regex(@"(<p>|</p>|<h1>|<h2>|<h3>|<h3>|</h1>|</h2>|</h3>|</h3>|<strong>|</strong>)");
        var prodDesc = regex.Replace(p.Description, "");
        lblDescription.Text = prodDesc.Replace("<br /><br />", "<br />");
        lblColour.Text = p.Colours.Replace("<h3>", "").Replace("</h3>", "<br/>");


        //Product size icons
        if (p.ProductFixedSizes != null && p.ProductFixedSizes != "")
        {
            if (p.ProductFixedSizes.Length > 0)
            {
                //string[] sizeitems = p.ProductFixedSizes.Split(',');
                //string mystring = "0, 10, 20, 30, 100, 200";
                var divHtml = new System.Text.StringBuilder();

                string[] prodFixedSizes = p.ProductFixedSizes.Split(',');
                foreach (string prodFixedSize in prodFixedSizes)
                {
                    if (prodFixedSize.Length > 0)
                    {
                        pnlDesc.Visible = false;

                        System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        div.TagName = "sizes";
                        //div.InnerHtml = String.Format("{0}", word);
                        Image img = new Image();
                        img.ImageUrl = "~/images/Assets/" + prodFixedSize + ".png";
                        //img.AlternateText = "Test image";
                        div.Controls.Add(img);
                        plSizes.Controls.Add(div);
                    }
                }
            }
        }
        else
            lblSize.Text = p.Size;

        //Bind Images
        string imgs = p.ProductImages;
        string[] prodImgs = imgs.Split(',');
        //Remove any duplicates
        var prodImg = prodImgs.Distinct();

        ArrayList listItems = new ArrayList();
        foreach (string s in prodImg)
        {
            //System.Console.WriteLine(s);
            if (s.Length > 3)
            { 
                if (File.Exists(Server.MapPath("images/ProductImages/" + s)))
                {
                    listItems.Add("images/ProductImages/" + s);
                }
            }

        }
        lstStats.DataSource = listItems;
        lstStats.DataBind();


    }




    public static string SentenceCase(string input)
    {
        if (input.Length < 1)
            return input;

        string sentence = input.ToLower();
        return sentence[0].ToString().ToUpper() +
           sentence.Substring(1);
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int productId = Convert.ToInt32(Request.QueryString["productId"]);
        var adminURL = "~/Admin/Administration.aspx?EditProd=" + productId;
        Response.Redirect(adminURL);
    }
}
