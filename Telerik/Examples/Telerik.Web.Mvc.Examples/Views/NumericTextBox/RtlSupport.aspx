<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TextBoxFirstLookModelView>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

<div class="t-rtl">
    <%= Html.Telerik().NumericTextBox()
            .Name("NumericTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42) 
    %>
    <br /><br />
    <%= Html.Telerik().CurrencyTextBox()
            .Name("CurrencyTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42)
    %>
    <br /><br />
    <%= Html.Telerik().PercentTextBox()
            .Name("PercentTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42)
    %>
    <br /><br />
    <%= Html.Telerik().IntegerTextBox()
            .Name("IntegerTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42)
%>
</div>

</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
</asp:content>