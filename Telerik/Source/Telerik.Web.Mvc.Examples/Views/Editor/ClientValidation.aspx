﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<EmployeeDto>" %>

<asp:content contentplaceholderid="MainContent" runat="server">

    <% Html.EnableClientValidation(); %>
    
    <% using (Html.BeginForm()) { %>
        <div class="editing-section">
            <div class="section-title">Edit Customer</div>

            <ul id="field-list">
                <li class="field">
                    <%= Html.LabelFor(e => e.FirstName) %>
                    <%= Html.TextBoxFor(e => e.FirstName) %>
                    <div class="error"><%= Html.ValidationMessageFor(e => e.FirstName) %></div>
                </li>
                <li class="field">
                    <%= Html.LabelFor(e => e.LastName) %>
                    <%= Html.TextBoxFor(e => e.LastName) %>
                    <div class="error"><%= Html.ValidationMessageFor(e => e.LastName) %></div>
                </li>
                <li class="field">
                    <%= Html.LabelFor(e => e.Notes)%>
                    <%= Html.Telerik().EditorFor(e => e.Notes)
                            .Name("Notes")
                            .Encode(false)
                            .HtmlAttributes(new { style = "float: left; width: 345px;" })
                            .Tools(tools => tools
                                .Clear()
                                .Bold().Italic().Underline()
                                .Separator()
                                .CreateLink().Unlink()
                            )
                    %>
                    <div class="error"><%= Html.ValidationMessageFor(e => e.Notes) %></div>
                </li>
                <li class="action-row">
                    <button class="t-button" type="submit">Save</button>
                </li>
            </ul>
        </div>
    <% } %> 

</asp:content>

<asp:content contentplaceholderid="HeadContent" runat="server">

<% Html.Telerik().ScriptRegistrar()
                 .DefaultGroup(group => group
                     .Add("MicrosoftAjax.js")
                     .Add("MicrosoftMvcValidation.js")); %>

    <style type="text/css">
        .editing-section
        {
            width: 700px;
            margin: 0 auto;
        }
        
        .section-title
        {
            font: 24px Arial,Helvetica,sans-serif;
            border-bottom: 1px solid #989898;
        }
        
        #field-list
        {
            border-top: 1px solid #d1d1d1;
            list-style-type: none;
            margin-top: 0;
            padding: 40px 0 0;
        }
        
        #field-list .field
        {
            list-style-type: none;
            overflow: hidden;
            white-space: nowrap;
        }
        
        #field-list label
        {
            float: left;
            width: 120px; text-align: right; padding-right: 5px;
            vertical-align: top;
            padding-top: 2px;
        }
        
        #field-list input,
        #field-list textarea
        {
            font: normal 12px Tahoma;
        }
        
        #field-list .error
        {
            color: red;
            clear: both;
            margin-left: 125px;
            font: 10px Arial,Helvetica,sans-serif;
            height: 15px;
        }
        
        #field-list .action-row { width: 470px; padding-top: 1.5em; height: 2em; }
        #field-list .action-row .t-button { float:right; width:60px; }
    </style>

</asp:content>
