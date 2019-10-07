using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web.Security;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using iTextSharp.text.html.simpleparser;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //lbltest.Text = "Hello " + Context.User.Identity.Name;
        if (!Page.IsPostBack)
        {
            //var _users = new Users();
            //_users.RetrieveFromDatabase();
            //repUsers.DataSource = _users;
            //repUsers.DataBind();


            BindTopProducts();

        }
    }

    private void BindTopProducts()
    {
        Products p = new Products();
        p.GetProducts((int)Enumerations.ProductCat.Top);

        ListView_Products.DataSource = p;
        ListView_Products.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();


            message.To.Add("kraindi@aol.com");
            //message.To.Add("Gsraindi@aol.com");
            //var tomail = System.Configuration.ConfigurationManager.AppSettings["infoEmail"].ToString();
            //message.To.Add(tomail);
            message.Subject = "HO Website Email: " + subjectTextBox.Text;
            message.IsBodyHtml = true;
            //message.Body = bodyTextBox.Text;

            message.Body = "<html><head></head><body>" +
            "<p></p>" +
            "<p>Name: " + nameTextBox.Text + "</p>" +
            "<p>Email: " + emailTextBox.Text + "</p>" +
            "<p>Message: " + bodyTextBox.Text + "</p>" +
            "</body></html>";

            smtpClient.Send(message);

            lblMessage.Text = "Thankyou your message has been submitted";
            nameTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            subjectTextBox.Text = string.Empty;
            bodyTextBox.Text = string.Empty;
        }
        catch
        { lblMessage.Text = "Error sending message"; }
    }

    protected void imgPaypal_Click(object sender, ImageClickEventArgs e)
    {
        string url = "";

        string business = "Satnamnandra@yahoo.com";  // your paypal email
        string description = "Donation";            // '%20' represents a space. remember HTML!
        string country = "UK";                  // AU, US, etc.
        string currency = "GBP";                 // AUD, USD, etc.

        url += "https://www.paypal.com/cgi-bin/webscr" +
            "?cmd=" + "_donations" +
            "&business=" + business +
            "&lc=" + country +
            "&item_name=" + description +
            "&currency_code=" + currency +
            "&bn=" + "PP%2dDonationsBF";

        System.Diagnostics.Process.Start(url);
    }


    //protected void repUsers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {

    //        bool paid = Convert.ToBoolean(((Label)e.Item.FindControl("lblPaid")).Text);

    //        if (paid == true)
    //            ((Image)e.Item.FindControl("imgPaid")).ImageUrl = @"images/greenpound.png";
    //        else
    //            ((Image)e.Item.FindControl("imgPaid")).ImageUrl = @"images/greypound.png";




    //    }
    //}
   
    /*
    protected void Button2_Click1(object sender, EventArgs e)
    {
  var p = new  PDFHelper();
 
    }
    

    private void CratePDaF()
    {
        String pdfDirectory = Server.MapPath(".") + @"\Resources\";
        String imgDirectory = Server.MapPath(".") + @"\images\";
        String imgMDirectory = Server.MapPath(".") + @"\images\ProductImages\";
        var g = Guid.NewGuid();

        using (FileStream fs = new FileStream(pdfDirectory + g + "SpacingTest.pdf", FileMode.Create))
        {



            Document doc = new Document(PageSize.A4, 2f, 2f, 2f, 2f);
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingAfter = 15f;

            PdfPTable table = new PdfPTable(3);
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.FixedHeight = 200f;
            cell.HorizontalAlignment = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);




            table.AddCell(" ");

            // add a image
            iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(imgDirectory + "hunter_small.gif");
            Logo.ScaleAbsolute(120f, 155.25f);
            table.AddCell(Logo);
            table.AddCell(" ");


            table.AddCell(" ");


            PdfPCell cell1 = new PdfPCell(new Phrase("Hunter-Outdoor"));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.Border = Rectangle.NO_BORDER;
            table.AddCell(cell1);
            //table.AddCell("Hunter-Outdoor");
            table.AddCell(" ");


            table.AddCell(" ");

            PdfPCell cell2 = new PdfPCell(new Phrase("BROCHURE " + DateTime.Now.ToShortDateString()));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Border = Rectangle.NO_BORDER;
            table.AddCell(cell2);
            //table.AddCell("BROCHURE " + DateTime.Now.ToShortDateString());
            table.AddCell(" ");
            //bro.Append("<div class=\"blockPad\"><h2>Hunter-Outdoor</h2><p>BROCHURE " + DateTime.Now.ToShortDateString() + "</p>");


            paragraphTable1.Add(table);
            doc.Add(paragraphTable1);

            doc.NewPage();


            //Products page builder
            Products prods = new Products();
            prods.GetProducts(null);

            int isNewPage = 0;
            int pageNo = 0;
            string prodName = string.Empty;
            string prodDesc = string.Empty;
            string prodImg = string.Empty;
            string prodName2 = string.Empty;
            string prodDesc2 = string.Empty;
            string prodImg2 = string.Empty;
            string errorImg = "errorImage.png";
            foreach (Product p in prods)
            {
               
                isNewPage++;

                if (isNewPage == 1)
                {
                    prodName = p.ProductName;
                    prodDesc = p.Description;
                    prodImg = p.MainImage;

                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(<br />|<br/>|</ br>|</br>)");
                    System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"(<p>|</p>|<h1>|<h2>|<h3>|<h3>|</h1>|</h2>|</h3>|</h3>|<div>|</div>)");

                    // Replace new line with <br/> tag
                    prodDesc = regex.Replace(prodDesc, "\r\n");
                    prodDesc = regex2.Replace(prodDesc, "\r\n");


                }
                else if (isNewPage == 2)
                {
                    pageNo++;

                    prodName2 = p.ProductName;
                    prodDesc2 = p.Description;
                    prodImg2 = p.MainImage;

                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(<br />|<br/>|</ br>|</br>)");
                    System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"(<p>|</p>|<h1>|<h2>|<h3>|<h3>|</h1>|</h2>|</h3>|</h3>|<div>|</div>)");

                    // Replace new line with <br/> tag
                    prodDesc2 = regex.Replace(prodDesc2, "\r\n");
                    prodDesc2 = regex2.Replace(prodDesc2, "\r\n");
                    
                }

                if (isNewPage == 2)
                {
                     
                    PdfPTable tblPro = new PdfPTable(3);

                    tblPro.HorizontalAlignment = 0;
                    tblPro.TotalWidth = 830f;
                    //tblPro.WidthPercentage = 100;
                    tblPro.LockedWidth = true;
                    float[] widths = new float[] { 410f, 10f, 410f };
                    tblPro.SetWidths(widths);

                    //Logo for header
                    Logo.ScaleAbsolute(100f, 75f);
                    PdfPCell headerCell = new PdfPCell(Logo);
                    headerCell.FixedHeight = 50f;
                    headerCell.Colspan = 2;
                    headerCell.Border = Rectangle.NO_BORDER;
                    headerCell.BackgroundColor = BaseColor.BLACK;
                    tblPro.AddCell(headerCell);


                    var FontColour = new BaseColor(31, 73, 125);
                    var MyFont = FontFactory.GetFont("Times New Roman", 11, FontColour);

                    var pageHeaderCell = new PdfPCell(new Phrase("Page "+pageNo, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.WHITE)));
                    pageHeaderCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                    pageHeaderCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pageHeaderCell.BackgroundColor = BaseColor.BLACK;
                    pageHeaderCell.PaddingBottom = 10f;
                    pageHeaderCell.PaddingRight = 10f;
                    pageHeaderCell.Border = Rectangle.NO_BORDER;

                    tblPro.AddCell(pageHeaderCell);
                    //End Header



                    // 1st column Product 
                    iTextSharp.text.Image mainImg;
                    try { mainImg = iTextSharp.text.Image.GetInstance(imgMDirectory + prodImg); }
                    catch { mainImg = iTextSharp.text.Image.GetInstance(imgMDirectory + errorImg); }

                    mainImg.ScaleToFit(250f, 290f);

                    PdfPCell imgCell1 = new PdfPCell();
                    imgCell1.Border = Rectangle.NO_BORDER;

                    Phrase phProdName = new Phrase();
                    phProdName.Add(new Phrase(prodName));
                    imgCell1.AddElement(phProdName);
                    imgCell1.AddElement(mainImg);
                    imgCell1.PaddingLeft = 20f;
                    imgCell1.PaddingTop = 10f;
                    tblPro.AddCell(imgCell1);
                    //End 1st column product


                    //Middle spacer
                    PdfPCell topMidSpacer = new PdfPCell(new Phrase(" "));
                    topMidSpacer.FixedHeight = 340f;
                    topMidSpacer.Border = Rectangle.NO_BORDER;
                    tblPro.AddCell(topMidSpacer);
                    //End middle spacer


                    // 1st column Product 
                    iTextSharp.text.Image mainImg2;
                    try { mainImg2 = iTextSharp.text.Image.GetInstance(imgMDirectory + prodImg2); }
                    catch { mainImg2 = iTextSharp.text.Image.GetInstance(imgMDirectory + errorImg); }

                    mainImg2.ScaleToFit(250f, 290f);

                    PdfPCell imgCell2 = new PdfPCell();
                    imgCell2.Border = Rectangle.NO_BORDER;

                    Phrase phProdName2 = new Phrase();
                    phProdName2.Add(new Phrase(prodName2));
                    imgCell2.AddElement(phProdName2);
                    imgCell2.AddElement(mainImg2);
                    imgCell2.PaddingLeft = 20f;
                    imgCell2.PaddingTop = 10f;
                    tblPro.AddCell(imgCell2);
                    //End 1st column product



                    //1st prod description
                    PdfPCell cellProdDesc1 = new PdfPCell(new Phrase(prodDesc, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                    cellProdDesc1.BorderColor = BaseColor.LIGHT_GRAY;
                    tblPro.AddCell(cellProdDesc1);
                    //End 1st prod description

                    PdfPCell bottomSpacer = new PdfPCell(new Phrase(" "));
                    bottomSpacer.FixedHeight = 190f;
                    bottomSpacer.Border = Rectangle.NO_BORDER;
                    tblPro.AddCell(bottomSpacer);

                    //2nd prod description
                    PdfPCell cellProdDesc2 = new PdfPCell(new Phrase(prodDesc2, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                    cellProdDesc2.BorderColor = BaseColor.LIGHT_GRAY;
                    tblPro.AddCell(cellProdDesc2);
                    //End 2nd prod description

                    Paragraph proPara = new Paragraph();
                    proPara.SpacingAfter = 15f;

                    proPara.Add(tblPro);
                    doc.Add(proPara);
                }

                //Create a new page after 2 products
                if (isNewPage == 2)
                {
                    isNewPage = 0;
                    doc.NewPage();
                }
            }


            doc.Close();
        }
    }

    */


   
}

