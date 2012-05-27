<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <%  Html.Telerik().Splitter().Name("Splitter1")
            .HtmlAttributes(new { style = "height: 300px;" })
            .Orientation(SplitterOrientation.Vertical)
            .Panes(vPanes =>
            {
                vPanes.Add()
                    .Size("100px")
                    .Collapsible(true)
                    .Content(() =>
                    {%>
                        <p>Outer splitter : top pane</p>
                    <%});

                vPanes.Add()
                    .Scrollable(false)
                    .Content(() =>
                    {%><% Html.Telerik().Splitter().Name("Splitter2")
                            .HtmlAttributes(new { style = "height: 100%; width: 100%; border: 0; overflow: hidden;" })
                            .Orientation(SplitterOrientation.Horizontal)
                            .Panes(hPanes =>
                            {
                                hPanes.Add()
                                    .Size("150px")
                                    .Collapsible(true)
                                    .Content(() =>
                                    {%>
                                        <p>Inner splitter :: left pane</p>
                                    <%});

                                hPanes.Add()
                                    .Content(() =>
                                    {%>
                                        <p>Inner splitter :: center pane</p>
                                    <%});

                                hPanes.Add()
                                    .Size("20%")
                                    .Collapsible(true)
                                    .Content(() =>
                                    {%>
                                        <p>Inner splitter :: right pane</p>
                                    <%});
                            })
                            .Render();
                    %><%});

                vPanes.Add()
                    .Size("20%")
                    .Collapsible(true)
                    .Content(() =>
                    {%>
                        <p>Outer splitter : bottom pane</p>
                    <%});
            })
            .Render();
    %>

</asp:Content>
