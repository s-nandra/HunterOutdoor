using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for PDFHelper
/// </summary>
public class PDFHelper
{
    public PDFHelper()
    {
        CreatePDF();
        //
        // TODO: Add constructor logic here
        //
    }

    public void CreatePDF()
    {
        String pdfDirectory = HttpContext.Current.Server.MapPath("~") + @"\Resources\";
        String imgDirectory = HttpContext.Current.Server.MapPath("~") + @"\images\";
        String imgMDirectory = HttpContext.Current.Server.MapPath("~") + @"\images\ProductImages\";
        String imgSize = HttpContext.Current.Server.MapPath("~") + @"\images\ProductImages\childsizes.jpg";

        using (FileStream fs = new FileStream(pdfDirectory + "RKBrochure.pdf", FileMode.Create))
        {

            Document doc = new Document(PageSize.A4, 2f, 2f, 1f, 1f);
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingAfter = 15f;

            PdfPTable table = new PdfPTable(3);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.FixedHeight = 200f;
            cell.HorizontalAlignment = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
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
            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell1);
            //table.AddCell("Hunter-Outdoor");
            table.AddCell(" ");


            table.AddCell(" ");

            PdfPCell cell2 = new PdfPCell(new Phrase("BROCHURE " + DateTime.Now.ToShortDateString()));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
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


            PdfPTable tblbro = new PdfPTable(3);


            foreach (Product p in prods)
            {

                isNewPage++;

                if (isNewPage == 1)
                {
                    AddProductsToBro(imgMDirectory, tblbro, p, isNewPage);


                }
                //else if (isNewPage == 2)
                //{
                //    pageNo++;


                //    prodDesc2 = p.Description + "<br/>" + p.Colours;
                //    prodImg2 = p.MainImage;
                //    prodSize2 = p.Size;

                //    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(<br />|<br/>|</ br>|</br>)");
                //    System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"(<p>|</p>|<h1>|<h2>|<h3>|<h3>|</h1>|</h2>|</h3>|</h3>|<div>|</div>)");

                //    // Replace new line with <br/> tag
                //    prodDesc2 = regex.Replace(prodDesc2, "\r\n");
                //    prodDesc2 = regex2.Replace(prodDesc2, "\r\n");

                //}

                if (isNewPage == 2)
                {
                    pageNo++;
                    PdfPTable tblPro = new PdfPTable(3);

                    AddBroHeader(Logo, pageNo, tblPro);

                    AddProductsToBro(imgMDirectory, tblbro, p, isNewPage);

                    doc.Add(tblPro);
                    doc.Add(tblbro);
                    tblbro = new PdfPTable(3);
                    //End Header

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

    private static void AddBroHeader(iTextSharp.text.Image Logo, int pageNo, PdfPTable tblPro)
    {
        tblPro.HorizontalAlignment = 0;
        tblPro.TotalWidth = 838f;
        //tblPro.WidthPercentage = 100;
        tblPro.LockedWidth = true;
        float[] widths = new float[] { 410f, 10f, 418f };
        tblPro.SetWidths(widths);

        //Logo for header
        Logo.ScaleAbsolute(100f, 75f);
        PdfPCell headerCell = new PdfPCell(Logo);
        headerCell.FixedHeight = 53f;
        headerCell.Colspan = 2;
        headerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        headerCell.BackgroundColor = BaseColor.BLACK;
        tblPro.AddCell(headerCell);


        var FontColour = new BaseColor(31, 73, 125);
        var MyFont = FontFactory.GetFont("Times New Roman", 11, FontColour);

        var pageHeaderCell = new PdfPCell(new Phrase("Page " + pageNo, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
        pageHeaderCell.VerticalAlignment = Element.ALIGN_BOTTOM;
        pageHeaderCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        pageHeaderCell.BackgroundColor = BaseColor.BLACK;
        pageHeaderCell.PaddingBottom = 10f;
        pageHeaderCell.PaddingRight = 10f;
        pageHeaderCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        tblPro.AddCell(pageHeaderCell);
        //End Header
    }

    private static void AddProductsToBro(String imgMDirectory, PdfPTable tblbro, Product p, int isNewPage)
    {

        tblbro.HorizontalAlignment = 0;
        tblbro.TotalWidth = 838f;
        //tblPro.WidthPercentage = 100;
        tblbro.LockedWidth = true;
        float[] tblwidths = new float[] { 280f, 284f, 274f };
        tblbro.SetWidths(tblwidths);

        Regex regex = new Regex(@"(<br />|<br/>|</ br>|</br>|<p>|</p>|<h1>|<h2>|<h3>|<h3>|</h1>|</h2>|</h3>|</h3>|<div>|</div>|<strong>|</strong>)");

        // Replace new line with <br/> tag
        var prodDesc = p.ProductName.ToUpper() + "<br/>" + p.Description;
        prodDesc = regex.Replace(prodDesc, "\r\n");

        p.Size = regex.Replace(p.Size, "\r\n");
        p.Colours = regex.Replace(p.Colours, "\r\n");

        // 1st column Product  
        iTextSharp.text.Image mainImg;
        try { mainImg = iTextSharp.text.Image.GetInstance(imgMDirectory + p.MainImage); }
        catch { mainImg = iTextSharp.text.Image.GetInstance(imgMDirectory + "errorImage.png"); }

        mainImg.ScaleToFit(250f, 230f);

        PdfPCell cellProductImage = new PdfPCell();

        cellProductImage.AddElement(mainImg);
        cellProductImage.PaddingLeft = 10f;
        cellProductImage.PaddingTop = 10f;
        if (isNewPage == 2)
        {
            cellProductImage.BorderColorTop = BaseColor.GRAY;
            cellProductImage.BorderWidthBottom = 0f;
            cellProductImage.BorderWidthLeft = 0f;
            cellProductImage.BorderWidthTop = 3f;
            cellProductImage.BorderWidthRight = 0f;
        }
        else
            cellProductImage.Border = iTextSharp.text.Rectangle.NO_BORDER;

        tblbro.AddCell(cellProductImage);
        //End 1st column product


        //DESCRIPTION
        PdfPCell cellDescription = new PdfPCell();
        cellDescription.BackgroundColor = new BaseColor(247, 247, 247);
        cellDescription.VerticalAlignment = Element.ALIGN_TOP;
        if (isNewPage == 2)
        {
            cellDescription.BorderColorTop = BaseColor.GRAY;
            cellDescription.BorderWidthBottom = 0f;
            cellDescription.BorderWidthLeft = 0f;
            cellDescription.BorderWidthTop = 3f;
            cellDescription.BorderWidthRight = 0f;
        }
        else
            cellDescription.Border = iTextSharp.text.Rectangle.NO_BORDER;
        Phrase phDesc = new Phrase();
        phDesc.Add(new Phrase(prodDesc, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
        cellDescription.AddElement(phDesc);
        cellDescription.FixedHeight = 270f;
        tblbro.AddCell(cellDescription);
        //End DESCRIPTION


        cellDescription = new PdfPCell();
        cellDescription.VerticalAlignment = Element.ALIGN_TOP;
        phDesc = new Phrase();

        if (p.ProductId == 21 || p.ProductId == 23)
        {
            cellDescription.PaddingTop = 10f;
            String imgKidsSize = HttpContext.Current.Server.MapPath("~") + @"\images\ProductImages\childsizes.jpg";
            var sizeImg = iTextSharp.text.Image.GetInstance(imgKidsSize);

            cellDescription.AddElement(sizeImg);

            phDesc.Add(new Phrase(p.Colours, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
            cellDescription.AddElement(phDesc);
            //dCell1.PaddingLeft = 20f;

        }
        else
        {
            if (p.ProductFixedSizes != null && p.ProductFixedSizes != "")
            {
                string[] prodFixedSizes = p.ProductFixedSizes.Split(',');

                Paragraph pr = new Paragraph();
                cellDescription.PaddingTop = 20f;

                foreach (string prodFixedSize in prodFixedSizes)
                {
                    if (prodFixedSize.Length > 0)
                    {
                        String imgS = HttpContext.Current.Server.MapPath("~") + @"\images\Assets\" + prodFixedSize + ".png";
                        var sizeFxdImg = iTextSharp.text.Image.GetInstance(imgS);
                        sizeFxdImg.ScaleToFit(30, 30);
                        sizeFxdImg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;

                        pr.Add(new Chunk(sizeFxdImg, 0, 0));
                    }
                }
                cellDescription.AddElement(pr);
                phDesc.Add(new Phrase(p.Colours, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                cellDescription.AddElement(phDesc);


            }
            else
            {
                phDesc.Add(new Phrase(p.Size + p.Colours, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                cellDescription.AddElement(phDesc);
            }

        }



        //topMidSpacer.FixedHeight = 340f;
        if (isNewPage == 2)
        {
            cellDescription.BorderColorTop = BaseColor.GRAY;
            cellDescription.BorderWidthBottom = 0f;
            cellDescription.BorderWidthLeft = 0f;
            cellDescription.BorderWidthTop = 3f;
            cellDescription.BorderWidthRight = 0f;
        }
        else
            cellDescription.Border = iTextSharp.text.Rectangle.NO_BORDER;

        tblbro.AddCell(cellDescription);
    }

    public void CreatePDF_OLDStyle()
    {
        String pdfDirectory = HttpContext.Current.Server.MapPath("~") + @"\Resources\";
        String imgDirectory = HttpContext.Current.Server.MapPath("~") + @"\images\";
        String imgMDirectory = HttpContext.Current.Server.MapPath("~") + @"\images\ProductImages\";
        String imgSize = HttpContext.Current.Server.MapPath("~") + @"\images\ProductImages\childsizes.jpg";

        using (FileStream fs = new FileStream(pdfDirectory + "RKBrochure.pdf", FileMode.Create))
        {



            Document doc = new Document(PageSize.A4, 2f, 2f, 2f, 2f);
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            Paragraph paragraphTable1 = new Paragraph();
            paragraphTable1.SpacingAfter = 15f;

            PdfPTable table = new PdfPTable(3);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.FixedHeight = 200f;
            cell.HorizontalAlignment = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
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
            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell1);
            //table.AddCell("Hunter-Outdoor");
            table.AddCell(" ");


            table.AddCell(" ");

            PdfPCell cell2 = new PdfPCell(new Phrase("BROCHURE " + DateTime.Now.ToShortDateString()));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell2);
            //table.AddCell("BROCHURE " + DateTime.Now.ToShortDateString());
            table.AddCell(" ");
            //bro.Append("<div class=\"blockPad\"><h2>Hunter-Outdoor</h2><p>BROCHURE " + DateTime.Now.ToShortDateString() + "</p>");


            paragraphTable1.Add(table);
            doc.Add(table);

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
            int prodid1 = 0;
            int prodid2 = 0;
            string prodSize1 = string.Empty;
            string prodSize2 = string.Empty;
            foreach (Product p in prods)
            {

                isNewPage++;

                if (isNewPage == 1)
                {
                    prodid1 = p.ProductId;
                    prodName = p.ProductName;
                    prodDesc = p.Description + "<br/>" + p.Colours;
                    prodImg = p.MainImage;
                    prodSize1 = p.Size;

                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(<br />|<br/>|</ br>|</br>)");
                    System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"(<p>|</p>|<h1>|<h2>|<h3>|<h3>|</h1>|</h2>|</h3>|</h3>|<div>|</div>)");

                    // Replace new line with <br/> tag
                    prodDesc = regex.Replace(prodDesc, "\r\n");
                    prodDesc = regex2.Replace(prodDesc, "\r\n");


                }
                else if (isNewPage == 2)
                {
                    pageNo++;

                    prodid1 = p.ProductId;
                    prodName2 = p.ProductName;
                    prodDesc2 = p.Description + "<br/>" + p.Colours;
                    prodImg2 = p.MainImage;
                    prodSize2 = p.Size;

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
                    headerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    headerCell.BackgroundColor = BaseColor.BLACK;
                    tblPro.AddCell(headerCell);


                    var FontColour = new BaseColor(31, 73, 125);
                    var MyFont = FontFactory.GetFont("Times New Roman", 11, FontColour);

                    var pageHeaderCell = new PdfPCell(new Phrase("Page " + pageNo, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    pageHeaderCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                    pageHeaderCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pageHeaderCell.BackgroundColor = BaseColor.BLACK;
                    pageHeaderCell.PaddingBottom = 10f;
                    pageHeaderCell.PaddingRight = 10f;
                    pageHeaderCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    tblPro.AddCell(pageHeaderCell);
                    //End Header



                    // 1st column Product 
                    iTextSharp.text.Image mainImg;
                    try { mainImg = iTextSharp.text.Image.GetInstance(imgMDirectory + prodImg); }
                    catch { mainImg = iTextSharp.text.Image.GetInstance(imgMDirectory + errorImg); }

                    mainImg.ScaleToFit(250f, 290f);

                    PdfPCell imgCell1 = new PdfPCell();
                    imgCell1.Border = iTextSharp.text.Rectangle.NO_BORDER;

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
                    topMidSpacer.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblPro.AddCell(topMidSpacer);
                    //End middle spacer


                    // 1st column Product 
                    iTextSharp.text.Image mainImg2;
                    try { mainImg2 = iTextSharp.text.Image.GetInstance(imgMDirectory + prodImg2); }
                    catch { mainImg2 = iTextSharp.text.Image.GetInstance(imgMDirectory + errorImg); }

                    mainImg2.ScaleToFit(250f, 290f);

                    PdfPCell imgCell2 = new PdfPCell();
                    imgCell2.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    Phrase phProdName2 = new Phrase();
                    phProdName2.Add(new Phrase(prodName2));
                    imgCell2.AddElement(phProdName2);
                    imgCell2.AddElement(mainImg2);
                    imgCell2.PaddingLeft = 20f;
                    imgCell2.PaddingTop = 10f;
                    tblPro.AddCell(imgCell2);
                    //End 1st column product


                    //1st prod description
                    /*PdfPCell cellProdDesc1 = new PdfPCell(new Phrase(prodDesc, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                    cellProdDesc1.BorderColor = BaseColor.LIGHT_GRAY;
                    cellProdDesc1.PaddingLeft = 5f;
                    cellProdDesc1.PaddingTop = 10f;
                    tblPro.AddCell(cellProdDesc1);*/


                    PdfPCell dCell1 = new PdfPCell();
                    dCell1.BorderColor = BaseColor.LIGHT_GRAY;

                    Phrase phD1 = new Phrase();
                    phD1.Add(new Phrase(prodDesc, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                    dCell1.AddElement(phD1);

                    if (prodid1 == 21 || prodid1 == 23)
                    {
                        var sizeImg = iTextSharp.text.Image.GetInstance(imgSize);

                        dCell1.AddElement(sizeImg);
                        //dCell1.PaddingLeft = 20f;
                        //dCell1.PaddingTop = 10f;
                    }
                    else
                    {
                        Phrase phProdd1 = new Phrase();
                        phProdd1.Add(new Phrase(prodSize1));
                        dCell1.AddElement(phProdd1);

                    }
                    tblPro.AddCell(dCell1);

                    //End 1st prod description

                    PdfPCell bottomSpacer = new PdfPCell(new Phrase(" "));
                    bottomSpacer.FixedHeight = 190f;
                    bottomSpacer.Border = Rectangle.NO_BORDER;
                    tblPro.AddCell(bottomSpacer);

                    //2nd prod description
                    /*PdfPCell cellProdDesc2 = new PdfPCell(new Phrase(prodDesc2, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                    cellProdDesc2.BorderColor = BaseColor.LIGHT_GRAY;
                    cellProdDesc2.PaddingLeft = 5f;
                    cellProdDesc2.PaddingTop = 10f;
                    tblPro.AddCell(cellProdDesc2);*/


                    PdfPCell dCell2 = new PdfPCell();
                    dCell2.BorderColor = BaseColor.LIGHT_GRAY;

                    Phrase phD2 = new Phrase();
                    phD2.Add(new Phrase(prodDesc2, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL, BaseColor.BLACK)));
                    dCell2.AddElement(phD2);

                    if (prodid2 == 21 || prodid2 == 23)
                    {
                        var sizeImg = iTextSharp.text.Image.GetInstance(imgSize);

                        dCell2.AddElement(sizeImg);
                        //dCell1.PaddingLeft = 20f;
                        //dCell1.PaddingTop = 10f;
                    }
                    else
                    {
                        Phrase phProdd2 = new Phrase();
                        phProdd2.Add(new Phrase(prodSize2));
                        dCell2.AddElement(phProdd2);

                    }
                    tblPro.AddCell(dCell2);


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
}