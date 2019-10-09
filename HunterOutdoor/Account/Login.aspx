<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <%--   <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink>
        if you don't have an account.--%>
    </p>
    <div>
        <table cellspacing="5" cellpadding="5">
            <tr>
                <td>
                    User
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" Width="200px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Password
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" Width="200px" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="cbxRememberMe" Text="Remember Me" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblMsg" runat="server"  ></asp:Label>
                </td>
                
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
