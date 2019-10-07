<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery.galleriffic.js" type="text/javascript"></script>
    <%--<link href="Styles/basic.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/galleriffic-2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.opacityrollover.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            // We only want these styles applied when javascript is enabled
            //            $('div.navigation').css({ 'width': '100%', 'float': 'left' });

            $('div.content').css('display', 'block');

            // Initially set opacity on thumbs and add
            // additional styling for hover effect on thumbs
            var onMouseOutOpacity = 0.67;
            $('#thumbs ul.thumbs li').opacityrollover({
                mouseOutOpacity: onMouseOutOpacity,
                mouseOverOpacity: 1.0,
                fadeSpeed: 'fast',
                exemptionSelector: '.selected'
            });

            // Initialize Advanced Galleriffic Gallery
            var gallery = $('#thumbs').galleriffic({
                delay: 2500,
                numThumbs: 15,
                preloadAhead: 10,
                enableTopPager: true,
                enableBottomPager: true,
                maxPagesToShow: 7,
                imageContainerSel: '#slideshow',
                controlsContainerSel: '#controls',
                captionContainerSel: '#caption',
                loadingContainerSel: '#loading',
                renderSSControls: true,
                renderNavControls: true,
                playLinkText: 'Play Slideshow',
                pauseLinkText: 'Pause Slideshow',
                prevLinkText: '&lsaquo; Previous Photo',
                nextLinkText: 'Next Photo &rsaquo;',
                nextPageLinkText: 'Next &rsaquo;',
                prevPageLinkText: '&lsaquo; Prev',
                enableHistory: false,
                autoStart: false,
                syncTransitions: true,
                defaultTransitionDuration: 900,
                onSlideChange: function (prevIndex, nextIndex) {
                    // 'this' refers to the gallery, which is an extension of $('#thumbs')
                    this.find('ul.thumbs').children()
							.eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
							.eq(nextIndex).fadeTo('fast', 1.0);
                },
                onPageTransitionOut: function (callback) {
                    this.fadeTo('fast', 0.0, callback);
                },
                onPageTransitionIn: function () {
                    this.fadeTo('fast', 1.0);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <asp:SiteMapPath ID="SiteMap1" runat="server">
    </asp:SiteMapPath>
    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />--%>
    <div id="page">
        
        <div id="container">
            <%--<h1>
                <a href="index.html">Galleriffic</a></h1>
            <h2>
                Thumbnail rollover effects and slideshow crossfades</h2>--%>
            <!-- Start Advanced Gallery Html Containers -->
            <asp:LinkButton ID="lnkEdit" Visible="false"  runat="server" onclick="lnkEdit_Click">EDIT</asp:LinkButton>
            <div style="clear: both;">
            </div>
            <div id="gallery" class="content">
                <div id="controls" class="controls">
                </div>
                <div class="slideshow-container">
                    <div id="loading" class="loader">
                    </div>
                    <div id="slideshow" class="slideshow">
                    </div>
                </div>
                <div id="caption" class="caption-container">
                </div>
            </div>
            <div id="thumbs" class="navigation">
                <asp:ListView ID="lstStats" runat="server">
                    <LayoutTemplate>
                        <ul class="thumbs noscript">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                        
                        <a class="thumb" name="leaf" href='<%# Container.DataItem %>' title="Title #0">
                            <img src='<%# Container.DataItem %>' alt="Title #0" width="100px" height="150px" />
                        </a>
                            <%--<div class="caption">
                                <div class="download">
                                    <a href="http://farm4.static.flickr.com/3261/2538183196_8baf9a8015_b.jpg">Download Original</a>
                                </div>
                                <div class="image-title"> Title #0</div>
                                <div class="image-desc"><asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label></div>
                            </div>--%>
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <p>
                            Nothing here.</p>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>

             <div class="clear">
            </div>
            <%--        <div class="image-desc"></div>--%>
            
            
            <div class="prodDesc">
                <div class="prodTitle">
                    <asp:Label ID="lblProdTitle" runat="server"></asp:Label>
                </div>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </div>
            <div class="prodDesc">
                  <div class="prodTitle">
                    <asp:Label ID="Label1" runat="server">Colours</asp:Label>
                </div>
                    <asp:Label ID="lblColour" runat="server"></asp:Label> 
            </div>
            <div class="prodDesc">
                <asp:Panel runat="server" ID="pnlDesc">
                    <div class="prodTitle">
                        <asp:Label ID="Label2" runat="server">Sizes</asp:Label>
                    </div>
                    <asp:Label ID="lblSize" runat="server"></asp:Label>
                </asp:Panel>
                <asp:PlaceHolder ID="plSizes" runat="server"></asp:PlaceHolder>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
 
    <div class="clear"></div>
  <%--  <div id="footer">
    </div>--%>
<%--    <asp:DataList ID="dtlist" runat="server" RepeatColumns="4" CellPadding="5">
        <ItemTemplate>
            
            <asp:Image Width="100" ID="Image2" ImageUrl='<%# Container.DataItem %>' runat="server" />
        </ItemTemplate>
    </asp:DataList>
    <asp:Panel ID="AdDetailsPanel" runat="server">
        <h4>
            <asp:Label ID="AdTitleLabel" runat="server"></asp:Label></h4>
        <h5>
            <asp:Label ID="AdTypeLabel" runat="server" />
            <asp:Label ID="AdPriceLabel" runat="server" /></h5>
        <asp:Panel ID="PhotoPanel" runat="server" Visible="False">
          
        </asp:Panel>
     
    </asp:Panel>--%>
    
    <div class="clear"></div>
</asp:Content>
