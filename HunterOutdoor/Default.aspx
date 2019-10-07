<%@ Page Title="Hunter-Outdoor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Hunter-Outdoor</title>
    <style>
        /* Prevents slides from flashing */
        #slides
        {
            display: none;
        }
    </style>
    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="Scripts/jquery.slides.min.js" type="text/javascript"></script>
    <%--  <link href="Styles/example.css" rel="stylesheet" type="text/css" />--%>
    <script>
        $(function () {
            $('#slides').slidesjs({
                width: 940,
                height: 528,
                play: {
                    active: true,
                    auto: true,
                    interval: 4000,
                    swap: true,
                    pauseOnHover: true,
                    restartDelay: 2500
                }
            });
        });
    </script>
    <!-- SlidesJS Optional: If you'd like to use this design -->
    <style>
        body
        {
            -webkit-font-smoothing: antialiased;
            color: #232525;
        }
        
        #slides
        {
            display: none;
        }
        
        #slides .slidesjs-navigation
        {
            margin-top: 5px;
        }
        
        a.slidesjs-next, a.slidesjs-previous, a.slidesjs-play, a.slidesjs-stop
        {
            background-image: url(img/btns-next-prev.png);
            background-repeat: no-repeat;
            display: block;
            width: 12px;
            height: 18px;
            overflow: hidden;
            text-indent: -9999px;
            float: left;
            margin-right: 5px;
        }
        
        a.slidesjs-next
        {
            margin-right: 10px;
            background-position: -12px 0;
        }
        
        a:hover.slidesjs-next
        {
            background-position: -12px -18px;
        }
        
        a.slidesjs-previous
        {
            background-position: 0 0;
        }
        
        a:hover.slidesjs-previous
        {
            background-position: 0 -18px;
        }
        
        a.slidesjs-play
        {
            width: 15px;
            background-position: -25px 0;
        }
        
        a:hover.slidesjs-play
        {
            background-position: -25px -18px;
        }
        
        a.slidesjs-stop
        {
            width: 18px;
            background-position: -41px 0;
        }
        
        a:hover.slidesjs-stop
        {
            background-position: -41px -18px;
        }
        
        .slidesjs-pagination
        {
            margin: 7px 0 0;
            float: right;
            list-style: none;
        }
        
        .slidesjs-pagination li
        {
            float: left;
            margin: 0 1px;
        }
        
        .slidesjs-pagination li a
        {
            display: block;
            width: 13px;
            height: 0;
            padding-top: 13px;
            background-image: url(images/Home/pagination.png);
            background-position: 0 0;
            float: left;
            overflow: hidden;
        }
        
        .slidesjs-pagination li a.active, .slidesjs-pagination li a:hover.active
        {
            background-position: 0 -13px;
        }
        
        .slidesjs-pagination li a:hover
        {
            background-position: 0 -26px;
        }
        
        #slides a:link, #slides a:visited
        {
            color: #333;
        }
        
        #slides a:hover, #slides a:active
        {
            color: #9e2020;
        }
        
        .navbar
        {
            overflow: hidden;
        }
    </style>
    <!-- End SlidesJS Optional-->
    <!-- SlidesJS Required: These styles are required if you'd like a responsive slideshow -->
    <style>
        #slides
        {
            display: none;
        }
        
        .container
        {
            margin: 0 auto;
        }
        
        /* For tablets & smart phones */
        @media (max-width: 767px)
        {
            body
            {
                padding-left: 20px;
                padding-right: 20px;
            }
            .container
            {
                width: auto;
            }
        }
        
        /* For smartphones */
        @media (max-width: 480px)
        {
            .container
            {
                width: auto;
            }
        }
        
        /* For smaller displays like laptops */
        @media (min-width: 768px) and (max-width: 979px)
        {
            .container
            {
                width: 724px;
            }
        }
        
        /* For larger displays */
        @media (min-width: 1200px)
        {
            .container
            {
                width: 1170px;
            }
        }
    </style>
    <!-- SlidesJS Required: -->
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <asp:SiteMapPath ID="SiteMap1" runat="server">
    </asp:SiteMapPath>
    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />--%>
    <div id="slides">
        <img src="images/Home/montage.jpg"></img>
        <img src="images/Home/montageBoulton.jpg"></img>
        <img src="images/Home/montageKids.jpg"></img>
    </div>
    <div class="contentContainer">
        <div class="block title">
            <div class="blockPad ">
                <h2>
                    Popular products</h2>
            </div>
            
        </div>
        <div class="block imgHome"> 
            <%--<img src="images/ProductImages/Aspen1.JPG" width="170" height="220" />
            <img src="images/ProductImages/Aspen1.JPG" width="170" height="220" />
             <img src="images/ProductImages/Aspen1.JPG" width="170" height="220" />
              <img src="images/ProductImages/Aspen1.JPG" width="170" height="220" />
               <img src="images/ProductImages/Aspen1.JPG" width="170" height="220" />--%>
            <asp:ListView ID="ListView_Products" runat="server" DataKeyNames="ProductID" GroupItemCount="5">
                <EmptyDataTemplate>
                    <table id="Table1" runat="server">
                        <tr>
                            <td>
                                No data was returned.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td id="Td1" runat="server" />
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server">
                        </td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td id="Td2" runat="server">
                        <table border="0">
                            <tr>
                                <td class="tdProdImg">
                                    <a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'>
                                        <image src='images/ProductImages/<%# Eval("MainImage") %>' width="170" height="210"
                                            border="0">
                                    </a>&nbsp
                                </td>
                            </tr>
                            <tr>
                                <td class="tdProdDesc">
                                    <a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'><span class="ProductListHead">
                                        <%# Eval("ProductName")%></span><br>
                                    </a>
                                    <%--   <span class="ProductListItem"><b>Special Price: </b></span>
                            <br /> <a href='AddToCart.aspx?productID=<%# Eval("ProductID") %>'><span class="ProductListItem">
                                <b>Add To Cart<b></font></span> </a>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table id="Table2" runat="server">
                        <tr id="Tr1" runat="server">
                            <td id="Td3" runat="server">
                                <table id="groupPlaceholderContainer" runat="server">
                                    <tr id="groupPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td id="Td4" runat="server">
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
        <div class="block full">
            <div class="blockPad center">
                <p>
                    We are a UK based manufacturers of Hunter-Outdoor and Styltex branded clothing.
                    Specialist in high class wax clothing
                </p>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="block">
            <div class="blockPad">
                <h3>
                    Contact Us</h3>
                <p>
                    K&K CLOTHING<br />
                    31-33 ANNE ROAD<br />
                    SMETHWICK<br />
                    BIRMINGHAM<br />
                    B66 2NZ<br />
                    ENGLAND<br />
                    Tel: +44 (0) 121 555 8334 (main)<br />
                    Fax: +44 (0) 121 565 3404<br />
                    Mob: +44 (0) 7855 595 097
                </p>
            </div>
        </div>
        <div class="block mid right">
            <div class="blockPad">
                <h3>
                    Email Us</h3>
                <div>
                    <p>
                        <table width="100%">
                            <tr>
                                <td class="style2">
                                    Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
                                    <span class="style3">*</span><asp:RequiredFieldValidator ID="nameReq" runat="server"
                                        ValidationGroup="Group1" Display="Dynamic" ControlToValidate="nameTextBox" SetFocusOnError="true"
                                        ErrorMessage="Please enter your name!" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Email Address:
                                </td>
                                <td>
                                    <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
                                    <span class="style3">*</span><asp:RequiredFieldValidator ID="emailReq" runat="server"
                                        ValidationGroup="Group1" Display="Dynamic" ControlToValidate="emailTextBox" SetFocusOnError="true"
                                        ErrorMessage="Please enter your email address!" />
                                    <asp:RegularExpressionValidator ID="emailExpressionValidator" runat="server" Display="Dynamic"
                                        ControlToValidate="emailTextBox" ValidationExpression="^\S+@\S+\.\S+$" ErrorMessage="You must enter a valid email address!"
                                        SetFocusOnError="true" ValidationGroup="Group1"> </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Subject:
                                </td>
                                <td>
                                    <asp:TextBox ID="subjectTextBox" runat="server"></asp:TextBox>
                                    <span class="style3">*</span><asp:RequiredFieldValidator ID="subjectReq" runat="server"
                                        ValidationGroup="Group1" Display="Dynamic" ControlToValidate="subjectTextBox"
                                        SetFocusOnError="true" ErrorMessage="Please enter a subject!" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Message:
                                </td>
                                <td>
                                    <asp:TextBox ID="bodyTextBox" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    <span class="style3">*</span><asp:RequiredFieldValidator ID="bodyReq" runat="server"
                                        ValidationGroup="Group1" Display="Dynamic" ControlToValidate="bodyTextBox" SetFocusOnError="true"
                                        ErrorMessage="Please enter a body!" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="#CC3300"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send" />
                                </td>
                            </tr>
                        </table>
                    </p>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
