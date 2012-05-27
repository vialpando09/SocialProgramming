<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
	
<div id="footer">
    <div id="terms">
		<a href="http://www.telerik.com/">www.telerik.com</a> |
		<a href="http://www.telerik.com/home/terms-of-use.aspx">Terms of Use</a> |
		<a href="http://www.telerik.com/corporate/contact-us.aspx">Contact Us</a>
    </div>

    <a id="telerik-logo" href="http://www.telerik.com/"><img src="<%= Url.Content("~/Content/Images/telerik-logo.png") %>" alt="Built by Telerik" width="91" height="21" /></a>
	<p id="copyright">
        Copyright 2002-<%= DateTime.Now.Year %> &copy; Telerik. All rights reserved<br />Telerik Inc,
		<span class="adr">
		    <span class="street-address">201 Jones Rd</span>,
		    <span class="locality">Waltham</span>,
		    <span class="region">MA</span>
		    <span class="postal-code">02451</span>
		</span>
    </p>
</div>