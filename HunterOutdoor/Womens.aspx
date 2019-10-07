<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Womens.aspx.cs" Inherits="Womens" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:SiteMapPath ID="SiteMap1" runat="server">
    </asp:SiteMapPath>
    <div id="breadcrumbBar">
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
    </div>
    <div class="clear">
    </div>
    <div id="prodSideLinks">
        <ul>
            <asp:Repeater runat="server" ID="repOrderedList">
                <ItemTemplate>
                    <li><a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'><span class="ProductListHead">
                        <%# Eval("ProductName")%></span><br>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div id="divWomens">
         <div class="catHeading">
            <h1>
                Women's Jackets &amp; Coats</h1>
            <div class="pagination">
                <asp:DataPager ID="lvDataPager1" runat="server" PagedControlID="ListView_Products">
                    <Fields>
                        <asp:NextPreviousPagerField ShowNextPageButton="False" ButtonCssClass="previousNextLink" />
                        <asp:NumericPagerField ButtonCount="16" ButtonType="Link" NumericButtonCssClass="numericLink" />
                        <asp:NextPreviousPagerField ShowPreviousPageButton="False" ButtonCssClass="previousNextLink" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
     
        <div class="clear">
        </div>
        <asp:ListView ID="ListView_Products" runat="server" DataKeyNames="ProductID" GroupItemCount="4"
            OnPagePropertiesChanging="ListView_Products_PagePropertiesChanging">
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
                                    <%-- <image src='Catalog/Images/Thumbs/<%# Eval("MainImage") %>' 
                      width="100" height="75" border="0">--%>
                                    <image src='images/ProductImages/<%# Eval("MainImage") %>' width="175" height="220"
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
        <div class="clear">
        </div>
         <div class="pagination">
                <asp:DataPager ID="lvDataPager2" runat="server" PagedControlID="ListView_Products">
                    <Fields>
                        <asp:NextPreviousPagerField ShowNextPageButton="False" ButtonCssClass="previousNextLink" />
                        <asp:NumericPagerField ButtonCount="16" ButtonType="Link" NumericButtonCssClass="numericLink" />
                        <asp:NextPreviousPagerField ShowPreviousPageButton="False" ButtonCssClass="previousNextLink" />
                    </Fields>
                </asp:DataPager>
            </div>
    </div>
</asp:Content>
