<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

    <div class="t-rtl">
        <%= Html.Telerik().Calendar()
                .Name("Calendar")
                .Value(DateTime.Today)
        %>
    </div>
    
</asp:content>