<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
<% Html.Telerik().TreeView()
        .Name("TreeView")
        .HtmlAttributes(new { style = "width: 300px; float: left; margin-bottom: 30px;" })
        .ShowCheckBox(true)
        .Items(treeViewItem =>
        {
            treeViewItem.Add().Text("UI Components")
                .Items(item =>
                {
                    item.Add().Text("ASP.NET WebForms");
                    item.Add().Text("Silverlight");
                    item.Add().Text("ASP.NET MVC");
                    item.Add().Text("WinForms");
                    item.Add().Text("WPF");
                })
                .Expanded(true);

            treeViewItem.Add().Text("Data")
                .Items(item =>
                {
                    item.Add().Text("OpenAccess ORM");
                    item.Add().Text("Reporting");
                });

            treeViewItem.Add().Text("TFS Tools")
                .Items(item =>
                {
                    item.Add().Text("Work Item Manager");
                    item.Add().Text("Project Dashboard");
                });

            treeViewItem.Add().Text("Automated Testing")
                .Items(item =>
                {
                    item.Add().Text("Web Testing Tools");
                });

            treeViewItem.Add().Text("ASP.NET CMS")
                .Items(item =>
                {
                    item.Add().Text("Sitefinity CMS");
                });
        })
        .Render(); %>
    
<% using (Html.Configurator("Client API").Begin()) { %>    
    
    <p>
        <button onclick="ExpandItem()" class="t-button">Expand</button> / <button onclick="CollapseItem()" class="t-button">Collapse</button>
        <br />
        <button onclick="ExpandAll()" class="t-button">Expand All</button> / <button onclick="CollapseAll()" class="t-button">Collapse All</button>
        <br />
        <button onclick="EnableItem()" class="t-button">Enable</button> / <button onclick="DisableItem()" class="t-button">Disable</button>
        <br />
          <button onclick="CheckItem()" class="t-button">Check</button>
        / <button onclick="UncheckItem()" class="t-button">Uncheck</button>
        / <button onclick="CheckAll()" class="t-button">Check all</button>
        / <button onclick="UncheckAll()" class="t-button">Uncheck all</button>
        <br />
        <%= Html.TextBox("itemText", "HTML5 Widgets", new { style = "width: 100px" })%> <button onclick="AppendItem()" class="t-button">Append</button>
        / <button onclick="RemoveItem()" class="t-button">Remove</button>
    </p>
<% } %>
        
    <script type="text/javascript">

        function ExpandItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = getSelected();

            treeView.expand(item);
        }

        function CollapseItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = getSelected();

            treeView.collapse(item);
        }

        function ExpandAll() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = $("> ul > li", treeView.element);

            treeView.expand(item);
        }

        function CollapseAll() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = $("> ul > li", treeView.element);

            treeView.collapse(item);
        }

        function EnableItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = getSelected();

            treeView.enable(item);
        }

        function DisableItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = getSelected();

            treeView.disable(item);
        }

        function CheckItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = getSelected();

            treeView.nodeCheck(item, true);
        }

        function CheckAll() {
            var treeView = $("#TreeView").data("tTreeView");

            treeView.nodeCheck(".t-item", true);
        }

        function UncheckItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var item = getSelected();

            treeView.nodeCheck(item, false);
        }

        function UncheckAll() {
            var treeView = $("#TreeView").data("tTreeView");

            treeView.nodeCheck(".t-item", false);
        }

        function AppendItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var itemData = { Text: $("#itemText").val() };
            var selected = getSelected();

            treeView.append(itemData, selected.length != 0 ? selected : null);
        }

        function RemoveItem() {
            var treeView = $("#TreeView").data("tTreeView");
            var itemToRemove = getSelected();

            treeView.remove(itemToRemove);
        }

        function getSelected() {
            return $('#TreeView .t-state-selected').closest('li');
        }
    </script>
            
</asp:content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        .example .configurator
        {
            width: 300px;
            float: left;
            margin: 0 0 0 10em;
            display: inline;
        }
        
        .configurator p
        {
            margin: 0;
            padding: .4em 0;
        }
        
        .configurator .t-button
        {
            display:inline-block;
            width:auto;
        }

    </style>
</asp:Content>