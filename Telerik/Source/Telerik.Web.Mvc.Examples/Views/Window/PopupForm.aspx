<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage<FeedbackMessage>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

	<%  Html.Telerik().Window()
            .Name("Window")
            .Title("Submit feedback")
            .Content(() => {%>
                <% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm("popupform", "window", FormMethod.Post, new { id = "feedback-form" }))
                   { %>
                        <p class="note">This is just an example, sent data will <strong>not</strong> be saved.</p>

                        <%= Html.LabelFor(model => model.Name) %>
                        <%= Html.TextBoxFor(model => model.Name) %>
                        <%= Html.ValidationMessageFor(model => model.Name) %>

                        <%= Html.LabelFor(model => model.Email) %>
                        <%= Html.TextBoxFor(model => model.Email) %>
                        <%= Html.ValidationMessageFor(model => model.Email) %>

                        <%= Html.LabelFor(model => model.Comment) %>
                        <%= Html.Telerik().EditorFor(model => model.Comment)
                                .Tools(tools => tools
                                    .Clear()
                                    .Bold().Italic().Separator()
                                    .InsertOrderedList().InsertUnorderedList().Separator()
                                    .Indent().Outdent().Separator()
                                    .CreateLink()
                                )%>
                        <%= Html.ValidationMessageFor(model => model.Comment) %>

                        <div class="form-actions">
                            <button type="submit" class="t-button">Submit feedback!</button>
                        </div>

                <% } %>
            <%})
            .Width(400)
            .Draggable(true)
            .Modal(true)
            .Visible(false)
            .Render();
	%>

    <button id="feedback-open-button" class="t-button">Submit feedback...</button>

    <% if (ViewData["message"] != null) {
           var message = ViewData["message"] as FeedbackMessage;
    %>
        <div class="t-group">
            <h3>Feedback:</h3>
    
            <p>
                Name: <%: message.Name %><br />
                E-mail: <%: message.Email %><br />
                Comment: <%: message.Comment %>
            </p>
        </div>
    <% } %>

    <% Html.Telerik().ScriptRegistrar()
           .OnDocumentReady(() =>
           {%>
               // open the initially hidden window when the button is clicked
               $('#feedback-open-button')
                    .click(function(e) {
                        e.preventDefault();
                        $('#Window').data('tWindow').center().open();
                    });
           <%}); %>

</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <% Html.Telerik().ScriptRegistrar()
             .DefaultGroup(group => group
                 .Add("MicrosoftAjax.js")
                 .Add("MicrosoftMvcValidation.js")); %>
    <style type="text/css">
        
        #feedback-open-button
        {
            height: 32px;
            margin: 2em 0 4em;
        }
        
        #feedback-form
        {
            padding: 0 1em 1em;
        }
        
        #feedback-form label
        {
            display: block;
            line-height: 25px;
            margin-top: 1em;
        }
        
        #feedback-form input
        {
            width: 370px;
        }
        
        .form-actions
        {
            padding-top: 1em;
            overflow: hidden;
        }
        
        .form-actions button
        {
            float: right;
        }
        
        .example .t-group
        {
            border-width: 1px;
            border-style: solid;
            padding: 0 1em 1em;
        }
        
    </style>

</asp:Content>
