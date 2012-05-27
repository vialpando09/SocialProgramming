<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <%  Html.Telerik().Splitter().Name("Splitter1")
            .Panes(panes =>
            {
                panes.Add()
                    .Size("200px")
                    .Collapsible(true)
                    .Content(() =>
                    {%>
                        <div style="padding: 0 2em; text-align: center">
                            <p>Static sidebar content</p>
                            <p>Right pane is loaded after the page loads</p>
                        </div>
                    <%});

                panes.Add()
                    .LoadContentFrom("AjaxView_Grid", "Splitter");
            })
            .Render();
    %>

    <% Html.Telerik().ScriptRegistrar()
           .DefaultGroup(group => group
               .Add("telerik.common.js")
               .Add("telerik.draganddrop.js")
               .Add("telerik.grid.js")
               .Add("telerik.grid.grouping.js")
            ); %>

</asp:Content>
