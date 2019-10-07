<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddUser.ascx.cs" Inherits="Admin_AddUser" %>
<table cellpadding="4" cellspacing="4" width="80%">
    <tr>
        <td>Firstname</td>
        <td>
            <asp:TextBox ID="txtFirstname" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Surname</td>
        <td>
            <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Email</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Password</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                onclick="btnSubmit_Click" /></td>
        
    </tr>
    <tr>
        <td colspan="2"><asp:Label ID="lblMessage" runat="server" ></asp:Label></td>
    </tr>
</table>
