<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="about-example">
    <h2>About this example <span class="viewengine-switch"><span class="selected-engine">ASPX</span><%= Html.ActionLink("Razor", (string)Html.ViewContext.RouteData.Values["action"], new { area = "razor", controller = (string)Html.ViewContext.RouteData.Values["controller"] }, new { @class = "t-link" })%></span></h2>

    <% Html.Telerik().TabStrip()
            .Name("code-viewer-tabs")
            .Items(tabstrip =>
           {
               var hasDescription = !string.IsNullOrEmpty(Convert.ToString(ViewData.Get<object>("Description")));

               if (hasDescription)
               {
                   tabstrip.Add()
                       .Text("Description")
                       .Content(() =>
                       {
                            %>
                            <div class="description"><%= ViewData["Description"] %></div>
                            <%
                       });
               }

               var codeFiles = ViewData.Get<Dictionary<string, string>>("codeFiles");

               foreach (var codeFile in codeFiles)
               {
                   tabstrip.Add()
                           .Text(codeFile.Key)
                           .LoadContentFrom("CodeFile", "Home", new { file = codeFile.Value });
               }
           })
            .SelectedIndex(0)
            .ClientEvents(events => events.OnLoad("codeTabLoad"))
            .Render(); 
    %>
</div>
