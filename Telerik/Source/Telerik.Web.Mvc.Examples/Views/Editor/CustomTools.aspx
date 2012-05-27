<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<EmployeeDto>" %>

<asp:content contentplaceholderid="MainContent" runat="server">

    <% Html.Telerik().Editor()
            .Name("Editor")
            .Value(() =>
            {   %>
                &lt;p&gt;
                    Telerik Editor for ASP.NET MVC allows your users to edit HTML in a familiar,
                    user-friendly way.&lt;br /&gt;
                    In this version, the Editor provides the core HTML editing engine, which includes
                    basic text formatting, hyperlinks, lists, and image handling.
                    The extension &lt;strong&gt;outputs identical HTML&lt;/strong&gt;
                    across all major browsers, follows accessibility standards
                    and provides an extended programming API for content manipulation.
                &lt;/p&gt;
                <% 
            })
            .Tools(tools => tools
                .Clear()
                .Custom(settings => settings.HtmlAttributes(new { @class = "t-html" }))
                .Separator()
                .Custom(settings => settings.Template(() =>
                    {
                        %><span>Choose a template: <%
                        Html.Telerik().DropDownList()
                            .Name("BackgroundTemplate")
                            .Items(items =>
                            {
                                items.Add().Text("Forest").Value("forest");
                                items.Add().Text("Sunset").Value("sunset");
                                items.Add().Text("Web 2.0").Value("web20");
                            })
                            .ClientEvents(events => events.OnChange("onTemplateChange"))
                            .Render();
                        %></span><%
                    }))
            )
            .Render();
    %>

    <script type="text/javascript">

        function onTemplateChange(e) {
            var editor = $('#Editor').data('tEditor');

            var colors = {
                forest: "#d3e0c2",
                sunset: "#fbfbc7",
                web20: "#c3d8f1"
            };

            $(editor.document.documentElement)
                .add(editor.document.body)
                .css("background", colors[e.value]);
        }

        var htmlSourcePopup;

        function showHtml(e) {
            e.stopPropagation();
            e.preventDefault();
        
            var editor = $('#Editor').data('tEditor');
            var html = editor.value();

            if (!htmlSourcePopup) {
                htmlSourcePopup =
                    $('<div class="html-view">' +
                          '<div class="textarea t-state-default"><textarea></textarea></div>' +
                          '<div class="t-button-wrapper">' + 
                              '<button id="htmlCancel" class="t-button">Cancel</button>' +
                              '<button id="htmlUpdate" class="t-button">Update</button>' +
                          '</div>' +
                      '</div>')
                    .css('display', 'none')
                  .tWindow({
                        title: 'View Generated HTML',
                        modal: true, 
                        resizable: false, 
                        draggable: true,
                        width: 700,
                        onLoad: function() {
                            var $popup = $(this);
                            $popup.find('.textarea')
                                    .css('width', function() {
                                        return 700 - (this.offsetWidth - $(this).width());
                                    })
                                    .focus()
                                  .end()
                                  .find('#htmlCancel')
                                    .click(function() {
                                        htmlSourcePopup.close();
                                    })
                                  .end()
                                  .find('#htmlUpdate')
                                    .click(function() {
                                        var value = $popup.find('textarea').val();
                                        editor.value(value);
                                        htmlSourcePopup.close();
                                    });
                        },
                        onClose: function() {
                            editor.focus();
                        },
                        effects: $.telerik.fx.toggle.defaults()
                })
                .data('tWindow');
            }

            $(htmlSourcePopup.element).find('textarea').text(html);

            htmlSourcePopup.center().open();
        }

    </script>

    <%-- attach handler for html viewer --%>
    <% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => { %>
            $('.t-html').click(showHtml);
            onTemplateChange({ value: $("#BackgroundTemplate").val() });
    <% }); %>

</asp:content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .t-editor .t-html
        {
            background-image: url('<%= Url.Content("~/Content/Editor/CustomTools/insert-html-icon.png") %>');
        }
        
        .html-view .t-button-wrapper
        {
            padding: .5em 0;
        }
        
        #htmlCancel
        {
            float: right;
        }
        
        .textarea
        {
            border-width: 1px;
            border-style: solid;
        }
        
        .textarea textarea
        {
             margin: 0;
             padding: 0;
             border: 0;
             font: normal 12px Consolas,Courier New,monospace;
             width: 100%;
             height: 300px;
        }
    </style>
</asp:Content>