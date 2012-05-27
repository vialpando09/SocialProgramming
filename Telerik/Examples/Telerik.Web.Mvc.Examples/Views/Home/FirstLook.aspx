<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
    <div id="overview">
        <div id="overview-spotlight">
            <h2 id="big-logo">Telerik Extensions for ASP.NET MVC</h2>
            
            <p>
                Telerik Extensions for ASP.NET MVC extend the ASP.NET MVC framework
                by delivering a server-based framework that integrates with
                client-side modules based on popular JavaScript library, jQuery.
            </p>
            
            <div id="greater-value">
                <h3>Get Greater Value</h3>
                <p>
                    Telerik Extensions for ASP.NET MVC are also available as part of Telerik Premium Collection with 8 other products.
                    <a href="http://www.telerik.com/purchase/individual-mvc.aspx" class="greater-value-read-more">See purchase options</a>
                </p>
            </div>
        </div>
    </div>
        
    <div id="product-first-glance"></div>
        
    <div id="overview-highlights" class="theme-agnostic">
        <h2>ASP.NET MVC Extensions Highlights</h2>

        <ul id="highlights" class="theme-agnostic-lead">
            <li>
                <h3>Pure ASP.NET MVC Components</h3>
                <p>Built on top of <a href="http://www.asp.net/mvc/">ASP.NET MVC</a> to leverage its values - lightweight rendering, clean HTML, separation of concerns, and testability.</p>
            </li>
            <li>
                <h3>Completely Open Source</h3>
                <p>The Extensions are licensed under the widely adopted <strong>GPLv3</strong>. A <a href="http://www.telerik.com/purchase/faqs/aspnet-mvc.aspx">commercial license with support</a> and access to service packs/weekly builds is also available.</p>
            </li>
            <li class="last">
                <h3>Exceptional Performance</h3>
                <p>No postbacks, no ViewState, and no page lifecycle. The Web Asset Managers optimize the delivery of CSS and JavaScript, so no precious HTTP requests are wasted.</p>
            </li>
            <li>
                <h3>Based on jQuery</h3>
                <p>Telerik Extensions draw on the power of <a href="http://jquery.com/">jQuery</a> for visual effects and DOM manipulations.</p>
            </li>
            <li>
                <h3>Search Engine Optimized</h3>
                <p>The Extensions render clean, semantic HTML, which is essential for indexing your content in the major search engines.</p>
            </li>
            <li class="last">
                <h3>Cross-browser Support</h3>
                <p>Telerik Extensions for ASP.NET MVC support all major browsers - Internet&nbsp;Explorer, Firefox, Safari, Opera and Google Chrome.</p>
            </li>
        </ul>

        <h2>ASP.NET MVC Showcase</h2>
        <ul id="showcase" class="theme-agnostic-lead">
            <li>
                <a href="http://demos.telerik.com/teampulsefeedbackportal/Project/1" title="Launch TeamPulse Ideas & Feedback Portal"><img src="<%= Url.Content("~/Content/Images/teampulse-portal.png") %>" alt="TeamPulse Ideas & Feedback Portal" /></a>
            </li>
            <li>
                <h3>TeamPulse Ideas & Feedback Portal</h3>
                <p>The TeamPulse Ideas & Feedback portal is a web-based solution for capturing and managing customer feedback which fully leverages Telerik’s Grid for ASP.NET MVC.</p><p><a href="http://demos.telerik.com/teampulsefeedbackportal/Project/1" title="Launch TeamPulse Ideas & Feedback Portal">Launch demo</a></p>
            </li>
            <li class="last">
                <p>The portal comes as an extension for our project management tool TeamPulse and is meant to help teams create better products and increase customer value.</p><p><a href="http://www.telerik.com/agile-project-management-tools/feedback-portal.aspx" title="More about TeamPulse Ideas & Feedback Portal">More about the portal</a></p>
            </li>
        </ul>

        <% Html.RenderPartial("GetMoreThanExpected"); %>
    </div>

	<div class="corner rc-topleft"></div>
	<div class="corner rc-topright"></div>
</asp:Content>
