<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddProduct.ascx.cs" Inherits="Admin_AddProduct" %>
<script src="../Scripts/tinymce/tinymce.min.js" type="text/javascript"></script>
<%--<script type="text/javascript">
    tinymce.init({
        selector: "textarea" ,
        encoding: "xml"

    });
</script>--%>
<script type="text/javascript">


    tinymce.init({
        selector: "textarea",
        encoding: "xml",
        force_br_newlines: true,
        force_p_newlines: false,
        forced_root_block: '' // Needed for 3.x

    });

    //    $("p").remove();


    $(function () {
        $('[name$="$mi"]').attr("name", $('[name$="$mi"]').attr("name"));

        $('[name$="$mi"]').click(function () {
            //set name for all to name of clicked 
            $('[name$="$mi"]').attr("name", $(this).attr("name"));
        });
    });

</script>
<asp:Panel ID="pnlOptions" runat="server">
    Pick Product:<asp:DropDownList ID="ddlProducts" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
    <asp:Button ID="btnPDF" runat="server" Text="Create PDF" OnClick="btnPDF_Click" />
</asp:Panel>
<asp:Label ID="lblProductOption" runat="server" ForeColor="#CC3300"></asp:Label>
<br />
<asp:Panel ID="pnlMain" Visible="false" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <div id="ism" class="ism">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <table width="100%" id="Table1" class="ismFixtureTable">
                    <tr>
                        <td>
                            Product Category
                        </td>
                        <td>
                            <asp:CheckBoxList ID="cbCat" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <%--            <tr>
                <td>Sub Category</td>
                <td>
                    <asp:DropDownList ID="ddlSubCategory" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
                    <tr class="ismFixture ismEven">
                        <td>
                            Product Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductName" Width="250px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Description
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="100%" TextMode="MultiLine"
                                Rows="20"></asp:TextBox>
                        </td>
                    </tr>
                    <%-- <tr class="ismFixture ismEven">
            <td valign="top">Sex</td>
            <td>
                <asp:RadioButtonList ID="rbSex" runat="server">
                    <asp:ListItem Text="Male" Value="1" />
                    <asp:ListItem Text="Female" Value="2" />
                    <asp:ListItem Text="Kids" Value="3" />
                    <asp:ListItem Text="Unisex" Value="4" />
                </asp:RadioButtonList>
            </td>
        </tr>--%>
                    <tr>
                        <td>
                            Colours
                        </td>
                        <td>
                            <asp:TextBox ID="txtColour" Width="250px" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="ismFixture ismEven">
                        <td>
                            Sizes
                        </td>
                        <td>
                            <asp:TextBox ID="txtSize" Width="250px" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="ismFixture ">
                        <td>
                            Fixed Sizes
                        </td>
                        <td>
                            <asp:CheckBoxList ID="cbSizes" runat="server" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnNext1" runat="server" Text="Next 1" OnClick="btnNext1_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:HiddenField ID="hdnProductid" runat="server" />
                <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                <table width="100%" id="ismFixtureTable" class="ismFixtureTable">
                    <%--  <tr>
                        <td>
                            Main image
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUploadControl" runat="server" />
                            <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" />
                            <br />
                            <br />
                            <asp:Label runat="server" ID="StatusLabel" Text="Upload status: " />
                            <asp:Label runat="server" ID="lblImageUploaded" />
                            <asp:LinkButton ID="lnkDeleteMain" runat="server" Text="Delete" OnClick="lnkDeleteMainButton_Click"></asp:LinkButton>
                        </td>
                    </tr>--%>
                    <tr class="ismFixture ismEven">
                        <td>
                            Add images <br /> (500w x 550h) Max
                        </td>
                        <td>
                            <asp:FileUpload ID="SubFileUploadControl" multiple="multiple" AllowMultiple="true"
                                runat="server" />
                            <asp:Button runat="server" ID="SybUploadButton" Text="Upload" OnClick="SubUploadButton_Click" />
                            <br />
                            <br />
                            <asp:Label runat="server" ID="SubStatusLabel" Text="Upload status: " />
                            <asp:Label runat="server" ID="lblImagesUploaded" />
                            <asp:Label runat="server" ID="lblSubImageUploaded" Visible="false" />
                            <br />
                            <table border="0" cellpadding="2" cellspacing="2">
                                <asp:Repeater ID="repImages" runat="server" Visible="false" EnableViewState="true"
                                    OnItemCommand="rep_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Container.DataItem %>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkDeleteSub" runat="server" Text="Delete" CommandName="SUB"
                                                    CommandArgument='<%# Container.DataItem %>'></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnBack" runat="server" Text="Back 1" OnClick="btnBack_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnNext2" runat="server" Visible="false" Text="Next 2" OnClick="btnNext2_Click" />
                            <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblMessage" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                Main Image set as:
                <asp:Label runat="server" ID="lblMainImage" /><br />
                <table width="100%" id="Table2" class="ismFixtureTable">
                    <tr>
                        <td colspan="2">
                            <table>
                                <asp:Repeater ID="repUploadedImaged" runat="server" OnItemCommand="repUploadedImaged_ItemCommand"
                                    OnItemDataBound="repUploadedImaged_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="cbxMain" Enabled="false" GroupName="mi" group="mi" runat="server">
                                                </asp:RadioButton><br />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblImageName" runat="server" Visible="false" Text='<%#Container.DataItem %>'></asp:Label>
                                                <image src='../images/ProductImages/<%#Container.DataItem %>' width="100" height="150"
                                                    border="1">
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSetMain" CommandArgument='<%#Container.DataItem %>' runat="server"
                                                    Text="Make main" CommandName="main" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" CommandArgument='<%#Container.DataItem %>' runat="server"
                                                    Text="Delete" CommandName="delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnBack2" runat="server" Text="Back 2" OnClick="btnBack2_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnFinish" runat="server" Text="Finish" OnClick="btnFinish_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View4" runat="server">
                <table width="100%" id="Table3" class="ismFixtureTable">
                    <tr>
                        <td>
                            Product Category
                        </td>
                        <td>
                            <asp:Label ID="lblSaveCat" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Product Name
                        </td>
                        <td>
                            <asp:Label ID="lblSavedProdName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Description
                        </td>
                        <td>
                            <asp:Label ID="lblSavedDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Colours
                        </td>
                        <td>
                            <asp:Label ID="lblSavedCol" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sizes
                        </td>
                        <td>
                            <asp:Label ID="lblSavedSizes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fixed Sizes
                        </td>
                        <td>
                            <asp:Label ID="lblSavedFixedSizes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <asp:Repeater ID="repSavedUploadedImaged" runat="server" OnItemDataBound="repUploadedImaged_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="cbxMain" Enabled="false" GroupName="mi" group="mi" runat="server">
                                                </asp:RadioButton><br />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblImageName" runat="server" Visible="false" Text='<%#Container.DataItem %>'></asp:Label>
                                                <image src='../images/ProductImages/<%#Container.DataItem %>' width="100" height="150"
                                                    border="1">
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View5" runat="server">
                <table width="100%">
                    <tr>
                        <td>
                            Are you sure you want to delete
                            <asp:Label ID="lblProduct" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete Product" OnClick="btnDeleteProduct_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblDeleteMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField ID="hdnAllImages" runat="server" />
</asp:Panel>
