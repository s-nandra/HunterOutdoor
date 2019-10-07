<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Administration.aspx.cs" Inherits="Administration" %>

<%@ Register Src="~/Admin/AddProduct.ascx" TagName="ucAddProduct" TagPrefix="controls" %>
<%@ Register Src="~/Admin/AddUser.ascx" TagName="ucAddUser" TagPrefix="controls" %>
<%@ Register Src="~/Admin/AddCategory.ascx" TagName="ucAddCategory" TagPrefix="controls" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });

        });

        $(function () {
            $("#tabs").tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
    <asp:SiteMapPath ID="SiteMap1" runat="server">
    </asp:SiteMapPath>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Add/Edit Product</a></li>
            <%--<li><a href="#tabs-2">User</a></li>--%>
           <%--  <li><a href="#tabs-3">Add Category</a></li>
                <li><a href="#tabs-4">Edit Product</a></li> --%>
        </ul>
        <div id="tabs-1">
            <controls:ucAddProduct ID="ucAddProduct" runat="server" />
        </div>
        <%-- <div id="tabs-2">
            <controls:ucAddUser ID="ucAddUser" runat="server" />
        </div>
      <div id="tabs-3">
            <controls:ucAddCategory ID="ucAddCategory" runat="server" />
        </div> --%>
    </div>
</asp:Content>
