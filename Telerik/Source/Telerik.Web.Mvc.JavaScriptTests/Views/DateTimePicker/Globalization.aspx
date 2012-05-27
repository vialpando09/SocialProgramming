<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" Culture="da-DK" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Globalization</h2>
    
    <script type="text/javascript">

        function getDateTimePicker() {
            return $('#DateTimePicker').data('tDateTimePicker');
        }

    </script>
    
    <%= Html.Telerik().DateTimePicker()
            .Name("DateTimePicker")
            .HtmlAttributes(new { style = "width:300px" })
            .Effects(e => e.Toggle()) 
    %>

    <% Html.Telerik().ScriptRegistrar().Globalization(true); %>

</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

        test('timeFormat property should be set to cultureInfo.shortTime by default', function() {
            var dateTimePicker = getDateTimePicker();
            equal(dateTimePicker.timeFormat, $.telerik.cultureInfo.shortTime, 'timeFormat');
        });

</script>

</asp:Content>