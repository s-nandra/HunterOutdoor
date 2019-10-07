<%@ Page Title="Accessories" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Spare.aspx.cs" Inherits="Spare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ListView ID="ListView_Products" runat="server" DataKeyNames="ProductID" GroupItemCount="4">
        <EmptyDataTemplate>
            <table id="Table1" runat="server">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td id="Td1" runat="server" />
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td id="Td2" runat="server">
                <table border="0" width="300">
                    <tr>
                        <td><a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'>
                            <%-- <image src='Catalog/Images/Thumbs/<%# Eval("MainImage") %>' 
                      width="100" height="75" border="0">--%>
                            <image src='images/ProductImages/<%# Eval("MainImage") %>' width="180" height="275" border="0">
                        </a>&nbsp </td>
                    </tr>
                    <tr>
                        <td><a href='ProductDetails.aspx?productID=<%# Eval("ProductID") %>'><span class="ProductListHead">
                            <%# Eval("ProductName")%></span><br>
                        </a>
                        
                  <%--      <span class="ProductListItem"><b>Special Price: </b></span>
                            <br />
                            <a href='AddToCart.aspx?productID=<%# Eval("ProductID") %>'><span class="ProductListItem">
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
                    <td id="Td4" runat="server"></td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
</asp:Content>
