﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Product>>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">
   <div class="panel">
        <label for="ComboBox-input">Combobox:</label>

        <%= Html.Telerik().ComboBox()
                .Name("ComboBox")
                .AutoFill(true)
                .BindTo(new SelectList(Model, "ProductID", "ProductName"))
                .HtmlAttributes(new { style = string.Format("width: 250px") })
                .Filterable(filtering =>
                {
                    filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                })
                .HighlightFirstMatch(true)
        %>
    
    </div>
    <div class="panel">
        <label for="AutoComplete">AutoComplete:</label>

        <%= Html.Telerik().AutoComplete()
                .Name("AutoComplete")
                .BindTo(Model.Select(p=>p.ProductName))
                .AutoFill(true)
                .HtmlAttributes(new { style = string.Format("width: 250px") })
                .HighlightFirstMatch(true)
                .Filterable(filtering =>
                {
                    filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                })
                .Encode(false)
        %>
    </div>

    <noscript>
        <p>Your browsing experience on this page will be better if you visit it with a JavaScript-enabled browser / if you enable JavaScript.</p>
    </noscript>

    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:content>
<asp:content contentPlaceHolderID="HeadContent" runat="server">
    
    <style type="text/css">
        
        .panel
        {
            padding: .3em 0;
        }
        
        .panel label
        {
            width: 100px;
            padding-right: .4em;
            display: inline-block;
            *display: inline;
            zoom: 1;
            text-align: right;
        }
    </style>
</asp:content>
