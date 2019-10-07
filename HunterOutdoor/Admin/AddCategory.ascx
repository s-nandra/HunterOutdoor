<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddCategory.ascx.cs" Inherits="Admin_AddCategory" %>
<div id="ism" class="ism">
    <table width="100%" id="ismFixtureTable" class="ismFixtureTable">
        <tr>
            <td>Category</td>
            <td>
                <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Lebel</td>
            <td>
                <asp:DropDownList ID="ddlLevel" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Level 1 Sub catgories</td>
            <td>
                <asp:CheckBoxList ID="cbCat" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label></td>
        </tr>
    </table>
</div>
