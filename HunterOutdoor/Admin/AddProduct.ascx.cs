using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
 

public partial class Admin_AddProduct : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            hdnProductid.Value = "0";
            PopulateLookups();

            if (Request.QueryString["EditProd"] != null)
            {
                int productId = Convert.ToInt32(Request.QueryString["EditProd"]);
                ddlProducts.SelectedValue = productId.ToString();
                btnSelect_Click(sender, e);
            }
        }
        txtDescription.Text = HttpUtility.HtmlDecode(txtDescription.Text);
        txtColour.Text = HttpUtility.HtmlDecode(txtColour.Text);
        txtSize.Text = HttpUtility.HtmlDecode(txtSize.Text);
    }

    private void PopulateLookups()
    {
        //categories checkbox
        var _cat = new Categories();
        _cat.GetCategories();

        foreach (Category t in _cat)
        {
            ListItem li = new ListItem(t.CatName, t.Id.ToString());
            this.cbCat.Items.Add(li);
        }


        //Fixed Sizes checkbox
        var _sizes = new Sizes();
        _sizes.GetSizes();

        foreach (Size s in _sizes)
        {
            ListItem li = new ListItem(s.SizeCode, s.SizeId.ToString());
            this.cbSizes.Items.Add(li);
        }


        var _pro = new Products();
        _pro.GetProducts(null);

        this.ddlProducts.Items.Add(new ListItem("New Product", "-1"));
        foreach (Product p in _pro)
        {
            ListItem li = new ListItem(p.ProductName, p.ProductId.ToString());
            this.ddlProducts.Items.Add(li);
        }



    }

    //protected void File_Upload(object sender, AjaxFileUploadEventArgs e)
    //{
    //    string filename = e.FileName;
    //    string strDestPath = Server.MapPath("~/images/Home/");


    //    DateTime dt = DateTime.Now;
    //    filename = dt.ToString("dMMMyyyyHHmm") + filename;

    //    AjaxFileUpload1.SaveAs(@strDestPath + filename);


    //}


    private void BindImages()
    {
        var MainImage = hdnAllImages.Value; //lblSubImageUploaded.Text;
        List<String> fn = new List<String>(MainImage.Split(','));
        fn = fn.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

        repUploadedImaged.Visible = true;
        repUploadedImaged.DataSource = fn;
        repUploadedImaged.DataBind();
    }

    private void SaveProduct()
    {
        if (txtProductName.Text != string.Empty)
        {
            var catOpt = "";
            catOpt = saveCbCatOptions(catOpt);

            var sizeOpt = "";
            sizeOpt = saveCbSizeOptions(sizeOpt);

            Product p = new Product();
            p.ProductName = txtProductName.Text;
            p.ProductCategory = catOpt;
            p.ProductFixedSizes = sizeOpt;
            p.ProductSubCategory = 0;//Convert.ToInt32(ddlSubCategory.SelectedValue);
            p.Description = HttpUtility.HtmlDecode(txtDescription.Text);  //txtDescription.Text;
            p.Colours = HttpUtility.HtmlDecode(txtColour.Text);
            p.Size = HttpUtility.HtmlDecode(txtSize.Text);

            //p.MainImage = lblImageUploaded.Text;
            p.MainImage = "";
            p.SubImages = lblSubImageUploaded.Text;

            int newid = p.InsertProduct();

            lblProductName.Text = newid.ToString() + " " + txtProductName.Text;

            hdnProductid.Value = newid.ToString();

            lblMessage.Text = "";



            BindProduct();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateProduct();
    }


    private void UpdateProduct(string setMainImg = null)
    {

        if (txtProductName.Text != string.Empty)
        {
            var catOpt = "";
            catOpt = saveCbCatOptions(catOpt);

            var sizeOpt = "";
            sizeOpt = saveCbSizeOptions(sizeOpt);

            Product p = new Product();

            p.ProductId = (Convert.ToInt32(hdnProductid.Value) > 0) ? Convert.ToInt32(hdnProductid.Value) : Convert.ToInt32(ddlProducts.SelectedValue);

            //p.ProductId = Convert.ToInt32(ddlProducts.SelectedValue);
            p.ProductName = txtProductName.Text;
            p.ProductCategory = catOpt;
            p.ProductFixedSizes = sizeOpt;
            p.ProductSubCategory = 0;// Convert.ToInt32(ddlSubCategory.SelectedValue);
            p.Description = HttpUtility.HtmlDecode(txtDescription.Text);  //txtDescription.Text;

            p.Description = p.Description.Replace("<div>", "");
            p.Description = p.Description.Replace("</div>", "");

            p.Colours = HttpUtility.HtmlDecode(txtColour.Text);
            p.Size = HttpUtility.HtmlDecode(txtSize.Text);

            if (setMainImg != null)
            {
                p.MainImage = setMainImg;
                setMainImg = null;
            }
            else
            {
                try
                {
                    if (lblMainImage.Text.Length > 1)
                        p.MainImage = lblMainImage.Text;
                    else
                    {
                        string[] imgs = lblSubImageUploaded.Text.Split(',');
                        p.MainImage = imgs[0];
                    }
                }
                catch { p.MainImage = ""; }
            }

            p.SubImages = lblSubImageUploaded.Text;
            BindUploadedImagesRepeater(p);

            p.UpdateProduct();
            BindProduct();
            BindImages();

            lblMessage.Text = "";

        }
    }
    private string saveCbCatOptions(string catOpt)
    {
        foreach (ListItem li in cbCat.Items)
            if (li.Selected)
                catOpt += li.Value + ",";
        return catOpt;
    }

    private string saveCbSizeOptions(string sizeOpt)
    {
        foreach (ListItem li in cbSizes.Items)
            if (li.Selected)
                sizeOpt += li.Value + ",";
        return sizeOpt;
    }
    /*
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
                {
                    

                    // Use current time

                    string fileName = FileUploadControl.FileName;

                    DateTime dt = DateTime.Now;
                    fileName = dt.ToString("dMMMyyyyHHmm") + fileName;

                    // Get the bitmap data from the uploaded file
                    //Bitmap src = Bitmap.FromStream(FileUploadControl.PostedFile.InputStream) as Bitmap;
                    using (Bitmap src = Bitmap.FromStream(FileUploadControl.PostedFile.InputStream) as Bitmap)
                    {
                        // Resize the bitmap data
                        Bitmap result = ProportionallyResizeBitmap(src, 400, 550);
                        string saveName = Server.MapPath("~/images/ProductImages/") + fileName;

                        result.Save(saveName, ImageFormat.Jpeg);
                        StatusLabel.Text = "Upload status: File uploaded! : ";
                        lblImageUploaded.Text = fileName;

                        result.Dispose();
                        src.Dispose();
                    }

                    
                }
                else
                    StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }



    }
    */
    /*
     protected void SubUploadButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (SubFileUploadControl.HasFile)
            {
                HttpFileCollection hfc = Request.Files;

                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        string sFileName = Path.GetFileName(hpf.FileName);

                        DateTime dt = DateTime.Now;
                        sFileName = dt.ToString("dMMMyyyyHHmm") + sFileName;

                        // Get the bitmap data from the uploaded file
                        //Bitmap src = Bitmap.FromStream(hpf.InputStream) as Bitmap;
                        Bitmap src = null;
                        //using (src = Bitmap.FromStream(hpf.InputStream,true,false) as Bitmap)
                        using (src = Bitmap.FromStream(hpf.InputStream) as Bitmap)
                        {

                            // Resize the bitmap data
                            Bitmap result = ProportionallyResizeBitmap(src, 400, 550);
                            if (hpf.ContentType == "image/jpeg")
                            {
                                string saveName = Server.MapPath("~/images/ProductImages/") + sFileName;

                                result.Save(saveName, ImageFormat.Jpeg);

                                SubStatusLabel.Text = "Upload status: File uploaded! : ";
                                lblImagesUploaded.Text += sFileName + ", ";
                                lblSubImageUploaded.Text += sFileName + ",";

                                UpdateProduct();
                                btnNext2.Visible = true;

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SubStatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        }

    }
 
    */
    protected void SubUploadButton_Click(object sender, EventArgs e)
    {
        lblImagesUploaded.Text = string.Empty;
        try
        {
            if (SubFileUploadControl.HasFile)
            {
                HttpFileCollection hfc = Request.Files;

                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        string sFileName = Path.GetFileName(hpf.FileName);

                        DateTime dt = DateTime.Now;
                        sFileName = dt.ToString("dMMMyyyyHHmm") + sFileName;

                        // Get the bitmap data from the uploaded file
                        //Bitmap src = Bitmap.FromStream(hpf.InputStream) as Bitmap;
                        Bitmap img = null;
                        //using (src = Bitmap.FromStream(hpf.InputStream,true,false) as Bitmap)
                        using (img = Bitmap.FromStream(hpf.InputStream) as Bitmap)
                        {

                            int imgWidth = img.Width;
                            int imgHeight = img.Height;

                            if (imgWidth < 501 && imgHeight < 551)
                            {
                                // Resize the bitmap data
                                // Bitmap result = ProportionallyResizeBitmap(src, 400, 550);
                                if (hpf.ContentType == "image/jpeg")
                                {
                                    string saveName = Server.MapPath("~/images/ProductImages/") + sFileName;

                                    img.Save(saveName, ImageFormat.Jpeg);

                                    SubStatusLabel.Text = "Upload status: File uploaded! : ";
                                    lblImagesUploaded.Text += sFileName + ", ";
                                    lblSubImageUploaded.Text += sFileName + ",";

                                    UpdateProduct();
                                    btnNext2.Visible = true;

                                }
                            }
                            else
                                lblImagesUploaded.Text += sFileName + ": Image width and height not within 400x550 <br/>";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SubStatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        }

    }


    public Bitmap ProportionallyResizeBitmap(Bitmap src, int maxWidth, int maxHeight)
    {

        // original dimensions
        int w = src.Width;
        int h = src.Height;

        // Longest and shortest dimension
        int longestDimension = (w > h) ? w : h;
        int shortestDimension = (w < h) ? w : h;

        // propotionality
        float factor = ((float)longestDimension) / shortestDimension;

        // default width is greater than height
        double newWidth = maxWidth;
        double newHeight = maxWidth / factor;

        // if height greater than width recalculate
        if (w < h)
        {
            newWidth = maxHeight / factor;
            newHeight = maxHeight;
        }

        // Create new Bitmap at new dimensions
        Bitmap result = new Bitmap((int)newWidth, (int)newHeight);
        using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
            g.DrawImage(src, 0, 0, (int)newWidth, (int)newHeight);

        return result;
    }


    protected void btnSelect_Click(object sender, EventArgs e)
    {
        pnlOptions.Visible = false;

        pnlMain.Visible = true;
        ddlProducts.Enabled = false;
        btnSelect.Visible = false;

        if (ddlProducts.SelectedValue != "-1" || Convert.ToInt32(hdnProductid.Value) > 0)
        {
            //Update
            BindProduct();
            lblProductOption.Text = "Editing Product: " + ddlProducts.SelectedItem;

            //btnSubmit.Visible = false;
        }
        else
        {
            //New product
            lblProductOption.Text = "Adding new product";
        }

    }

    private void BindProduct()
    {
        lblImagesUploaded.Text = string.Empty;

        var p = new Product();
        if (Convert.ToInt32(hdnProductid.Value) > 0)
            p.GetProduct(Convert.ToInt32(hdnProductid.Value));
        else
            p.GetProduct(Convert.ToInt32(ddlProducts.SelectedValue));

        hdnProductid.Value = p.ProductId.ToString();
        txtProductName.Text = p.ProductName;
        txtDescription.Text = p.Description;
        txtColour.Text = p.Colours;
        txtSize.Text = p.Size;
        //lblImageUploaded.Text = p.MainImage;
        lblMainImage.Text = p.MainImage;

        //if (!p.SubImages.Contains(lblMainImage.Text))
        //{
        //    lblSubImageUploaded.Text 
        //    //appendsndndnd here
        //}else

        lblSubImageUploaded.Text = p.SubImages;

        hdnAllImages.Value = p.ProductImages;

        if (p.ProductCategory != null)
        {
            string[] items = p.ProductCategory.Split(','); ;
            for (int i = 0; i < cbCat.Items.Count; i++)
                if (items.Contains(cbCat.Items[i].Value))
                    cbCat.Items[i].Selected = true;
        }

        if (p.ProductFixedSizes != null)
        {
            string[] sizeitems = p.ProductFixedSizes.Split(','); ;
            for (int i = 0; i < cbSizes.Items.Count; i++)
                if (sizeitems.Contains(cbSizes.Items[i].Value))
                    cbSizes.Items[i].Selected = true;
        }

        BindUploadedImagesRepeater(p);
    }

    private void BindUploadedImagesRepeater(Product p)
    {
        if (p.SubImages != null)
        {
            if (p.SubImages.Length > 2)
            {
                btnNext2.Visible = true;
                List<String> fn = new List<String>(hdnAllImages.Value.Split(','));
                fn = fn.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

                repImages.Visible = true;
                repImages.DataSource = fn;
                repImages.DataBind();
            }
        }
    }

    protected void lnkDeleteMainButton_Click(object sender, EventArgs e)
    {
        var p = new Product();
        p.DeleteProductImage(Convert.ToInt32(ddlProducts.SelectedValue), "MAIN");

        //lblImageUploaded.Text = "";
    }


    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("SUB"))
        {
            var prodName = e.CommandArgument.ToString();
            var p = new Product();
            if (prodName.Length > 1)
            {
                //p.DeleteProductImage(Convert.ToInt32(ddlProducts.SelectedValue), prodName);
                p.DeleteProductImage(Convert.ToInt32(hdnProductid.Value), prodName);

                string deletePath = Server.MapPath("~/images/ProductImages/") + prodName;
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
                else
                    repImages.Visible = false;

                BindProduct();
                BindImages();
            }

        }
    }

    protected void repUploadedImaged_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {


        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            var lblImageName = ((Label)e.Item.FindControl("lblImageName")).Text;
            if (lblImageName == lblMainImage.Text)
            {
                ((RadioButton)e.Item.FindControl("cbxMain")).Checked = true;
            }
        }
    }

    protected void repUploadedImaged_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        var prodName = e.CommandArgument.ToString();
        //Runs when you select the radio button in the repeater
        if (e.CommandName == "main")
            UpdateProduct(prodName);
        else if (e.CommandName == "delete")
        {
            var p = new Product();
            if (prodName.Length > 1)
            {
                p.DeleteProductImage(Convert.ToInt32(hdnProductid.Value), prodName);

                string deletePath = Server.MapPath("~/images/ProductImages/") + prodName;
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
                else
                    repImages.Visible = false;

                BindProduct();
                BindImages();
            }

        }


    }


 

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlProducts.SelectedValue) > 0)
        {
            pnlMain.Visible = true;
            ddlProducts.Enabled = false;
            btnSelect.Visible = false;
            BindProduct();
            MultiView1.ActiveViewIndex = 4;
        }

    }
    protected void btnDeleteProduct_Click(object sender, EventArgs e)
    {
        Product p = new Product();

        p.DeleteProduct(Convert.ToInt32(ddlProducts.SelectedValue));

        string[] imgs = hdnAllImages.Value.Split(','); ;

        foreach (string img in imgs)
        {
            string deletePath = Server.MapPath("~/images/ProductImages/") + img;
            if (File.Exists(deletePath))
                File.Delete(deletePath);
        }

        lblDeleteMsg.Text = "Product Deleted";
        pnlOptions.Visible = false;
        btnDeleteProduct.Visible = false;

    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        var p = new PDFHelper();
        btnPDF.Enabled = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnBack2_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void btnNext1_Click(object sender, EventArgs e)
    {
        if (ddlProducts.SelectedValue != "-1" || Convert.ToInt32(hdnProductid.Value) > 0)
            UpdateProduct();
        else
            SaveProduct();

        MultiView1.ActiveViewIndex += 1;
    }
    protected void btnNext2_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex += 1;
        BindImages();
        BindProduct();
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex += 1;
        BindSavedProduct();
    }

    private void BindSavedProduct()
    {
        var p = new Product();
        p.GetProduct(Convert.ToInt32(hdnProductid.Value));

        
        lblSavedProdName.Text = p.ProductName;
        //lblSaveCat.Text = p.ProductCategory;
        lblSavedDesc.Text = p.Description;
        lblSavedCol.Text = p.Colours;
        lblSavedSizes.Text = p.Size;
        lblSavedFixedSizes.Text = p.ProductFixedSizes;

        var MainImage = hdnAllImages.Value; //lblSubImageUploaded.Text;
        List<String> fn = new List<String>(MainImage.Split(','));
        fn = fn.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

        repSavedUploadedImaged.Visible = true;
        repSavedUploadedImaged.DataSource = fn;
        repSavedUploadedImaged.DataBind();

        var _cat = new Categories();
        _cat.GetCategories();

        p.ProductCategory = p.ProductCategory + "0";

        var prodCat = p.ProductCategory.Split(',').Select(int.Parse).ToArray();

        lblSaveCat.Text = string.Join("\r\n", _cat.Where(x => prodCat.Contains(x.Id)).Select(c => c.CatName));

         

         

        //string[] prodFixedSizes = p.ProductFixedSizes.Split(',');
        //foreach (string prodFixedSize in prodFixedSizes)
        //{
        //    if (prodFixedSize.Length > 0)
        //    {
        //        pnlDesc.Visible = false;

        //        System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
        //        div.TagName = "sizes";
        //        //div.InnerHtml = String.Format("{0}", word);
        //        Image img = new Image();
        //        img.ImageUrl = "~/images/Assets/" + prodFixedSize + ".png";
        //        //img.AlternateText = "Test image";
        //        div.Controls.Add(img);
        //        plSizes.Controls.Add(div);
        //    }
        //}
 
    }
}