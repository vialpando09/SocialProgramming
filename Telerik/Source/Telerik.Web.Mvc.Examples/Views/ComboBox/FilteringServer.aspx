﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Telerik.Web.Mvc.Examples.ComboBoxAjaxFilteringModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().ComboBox()
                      .Name("AjaxComboBox")
                      .HtmlAttributes(new { id = "AjaxComboBox_wrapper" })
                      .AutoFill(Model.ComboBoxAttributes.AutoFill.Value)
                      .DataBinding(binding => binding.Ajax()
                                                     .Select("_FilteringAjax", "ComboBox")
                                                     .Delay(Model.ComboBoxAttributes.Delay.Value)
                                                     .Cache(Model.ComboBoxAttributes.Cache.Value)
                      )
                      .Filterable(filtering =>
                      {
                          if (Model.ComboBoxAttributes.FilterMode != 0)
                          {
                              filtering.FilterMode((AutoCompleteFilterMode)Enum.Parse(typeof(AutoCompleteFilterMode), Model.ComboBoxAttributes.FilterMode.ToString()));
                              filtering.MinimumChars(Model.ComboBoxAttributes.MinimumChars.Value);
                          }
                      })
                      .HighlightFirstMatch(Model.ComboBoxAttributes.HighlightFirst.Value)
                      .ClientEvents(events => events.OnDataBinding("onComboBoxDataBinding"))
    %>

    <% using (Html.Configurator("The ComboBox should...")
                  .PostTo("FilteringServer", "ComboBox")
                  .Begin())
       { %>
        <ul>
            <li>
                have <strong>filter mode</strong>&nbsp;
                <% Html.Telerik().DropDownList()
                                 .Name("ComboBoxAttributes.FilterMode")
                                 .HtmlAttributes(new { style = "width:100px" })
                                 .Items(items =>
                                 {
                                     items.Add().Text("None").Value("0");
                                     items.Add().Text("StartsWith").Value("1");
                                     items.Add().Text("Contains").Value("2");
                                 })
                                 .SelectedIndex(Model.ComboBoxAttributes.FilterMode.Value)
                                 .ClientEvents(events =>
                                     events.OnChange(() =>
                                     {
                                     %>
                                         function(e) {
                                             var $textboxes = $('#ComboBoxAttributes_MinimumChars, #ComboBoxAttributes_Delay');
                                             $textboxes.each(function(){
                                                 var textbox = $(this).data('tTextBox');
                                                 if(e.value == 0) textbox.disable();
                                                 else textbox.enable();
                                             });
                                         }
                                     <%
                                     })
                                 ).Render();
                %>
            </li>
            <li>
                <label for="ComboBoxAttributes_MinimumChars"><strong>start filtering</strong> after</label>
                <% Html.Telerik().IntegerTextBoxFor(m => m.ComboBoxAttributes.MinimumChars)
                                 .InputHtmlAttributes(new { style = "width: 60px" })
                                 .MinValue(0)
                                 .MaxValue(1000)
                                 .ClientEvents(events =>
                                     events.OnLoad(() =>
                                     {                                
                                     %>
                                         function(e) {
                                             var dropDownList = $('#ComboBoxAttributes_FilterMode').data('tDropDownList');
                                             if(dropDownList.index == 0) $(this).data('tTextBox').disable();
                                         }
                                     <%
                                 })
                                 ).Render();
                %>
                <label for="ComboBoxAttributes_Delay">chars and </label>
                <% Html.Telerik().IntegerTextBoxFor(m => m.ComboBoxAttributes.Delay)
                                 .InputHtmlAttributes(new { style = "width: 60px" })
                                 .MinValue(0)
                                 .MaxValue(2000)
                                 .Value(Model.ComboBoxAttributes.Delay.Value)
                                 .ClientEvents(events =>
                                    events.OnLoad(() =>
                                    {                                
                                    %>
                                        function(e) {
                                            var dropDownList = $('#ComboBoxAttributes_FilterMode').data('tDropDownList');
                                            if(dropDownList.index == 0) $(this).data('tTextBox').disable();
                                        }
                                    <%
                                 })
                                 ).Render();               
                %>
                ms delay
            </li>
            <li>
                <label for="ComboBoxAttributes_Cache"><strong>cache</strong> last ajax request</label>
                <%= Html.CheckBox("ComboBoxAttributes.Cache", Model.ComboBoxAttributes.Cache.GetValueOrDefault(false), new { title = "Cache" })%>
            </li>
            <li>
                <label for="ComboBoxAttributes_HighlightFirst"><strong>highlight</strong> first item</label>
                <%= Html.CheckBox("ComboBoxAttributes.HighlightFirst", Model.ComboBoxAttributes.HighlightFirst.GetValueOrDefault(false), new { title = "HighlightFirst" })%>
            </li>
            <li>
                <label for="ComboBoxAttributes_AutoFill"><strong>auto-filling</strong> text</label>
                <%= Html.CheckBox("ComboBoxAttributes.AutoFill", Model.ComboBoxAttributes.AutoFill.GetValueOrDefault(false), new { title = "AutoFill" })%>
            </li>
        </ul>
        <button type="submit" class="t-button">Apply</button>
    <% } %>

    <%= Html.Telerik().AutoComplete()
                    .Name("AjaxAutoComplete")
                    .AutoFill(Model.AutoCompleteAttributes.AutoFill.Value)
                    .DataBinding(binding => binding.Ajax()
                                                   .Select("_AutoCompleteFilteringAjax", "ComboBox")
                                                   .Delay(Model.AutoCompleteAttributes.Delay.Value)
                                                   .Cache(Model.AutoCompleteAttributes.Cache.Value)
                    )
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode((AutoCompleteFilterMode)Enum.Parse(typeof(AutoCompleteFilterMode), Model.AutoCompleteAttributes.FilterMode.ToString()));
                        filtering.MinimumChars(Model.AutoCompleteAttributes.MinimumChars.Value);
                    })
                    .Multiple(multi => multi.Enabled(Model.AutoCompleteAttributes.AllowMultipleValues.Value))
                    .HighlightFirstMatch(Model.AutoCompleteAttributes.HighlightFirst.Value)
                    .ClientEvents(events => events.OnDataBinding("onAutoCompleteDataBinding"))
    %>

    <% using (Html.Configurator("The AutoComplete should...")
                  .PostTo("FilteringServer", "ComboBox")
                  .Begin())
       { %>
               <ul>
            <li>
                have <strong>filter mode</strong>&nbsp;
                <%= Html.Telerik().DropDownList()
                                  .Name("AutoCompleteAttributes.FilterMode")
                                  .HtmlAttributes(new { style = "width:100px" })
                                  .Items(items =>
                                  {
                                      items.Add().Text("StartsWith").Value("1");
                                      items.Add().Text("Contains").Value("2");
                                  })
                                  .SelectedIndex(Model.AutoCompleteAttributes.FilterMode.Value - 1)
                %>
            </li>
            <li>
                <label for="AutoCompleteAttributes_MinimumChars"><strong>start filtering</strong> after</label>
                <%= Html.Telerik().IntegerTextBoxFor(m => m.AutoCompleteAttributes.MinimumChars)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(1)
                                  .MaxValue(1000)
                %>
                <label for="AutoCompleteAttributes_Delay">chars and</label>
                <%= Html.Telerik().IntegerTextBoxFor(m => m.AutoCompleteAttributes.Delay)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(0)
                                  .MaxValue(2000)
                %>
                ms delay
            </li>
            <li>
                <label for="AutoCompleteAttributes_Cache"><strong>cache</strong> last ajax request</label>
                <%= Html.CheckBox("AutoCompleteAttributes.Cache", Model.AutoCompleteAttributes.Cache.GetValueOrDefault(false), new { title = "Cache" })%>
            </li>
            <li>
                <label for="AutoCompleteAttributes_HighlightFirst"><strong>highlight</strong> first item</label>
                <%= Html.CheckBox("AutoCompleteAttributes.HighlightFirst", Model.AutoCompleteAttributes.HighlightFirst.GetValueOrDefault(false), new { title = "HighlightFirst" })%>
            </li>
            <li>
                <label for="AutoCompleteAttributes_AutoFill"><strong>auto-filling</strong> text</label>
                <%= Html.CheckBox("AutoCompleteAttributes.AutoFill", Model.AutoCompleteAttributes.AutoFill.GetValueOrDefault(false), new { title = "AutoFill" })%>
            </li>
            <li>
                <label for="AutoCompleteAttributes_AllowMultipleValues">allow <strong>multiple</strong> values</label>
                <%= Html.CheckBox("AutoCompleteAttributes.AllowMultipleValues", Model.AutoCompleteAttributes.AllowMultipleValues.GetValueOrDefault(false), new { title = "AllowMultipleValues" })%>
            </li>
        </ul>
        <button type="submit" class="t-button">Apply</button>
    <% } %>

    <script type="text/javascript">
        
        function onComboBoxDataBinding(e) {
            e.data = $.extend({}, e.data, { filterMode: $('#ComboBoxAttributes_FilterMode').data('tDropDownList').value() });
        }

        function onAutoCompleteDataBinding(e) {
            e.data = $.extend({}, e.data, { filterMode: $('#AutoCompleteAttributes_FilterMode').data('tDropDownList').value() });
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        #AjaxComboBox_wrapper
        {
            margin-bottom: 280px;
            float: left;
        }
       
        #AjaxAutoComplete
        {
            clear: both;
            margin-bottom: 280px;
            float: left;
        }
       
        .example .configurator
        {
            width: 400px;
            float: left;
            margin: 0 0 0 15em;
            display: inline;
        }
        
        .configurator p
        {
            margin: 0;
            padding: .4em 0;
        }
        
        .configurator li
        {
            padding-bottom: 3px;
        }
        
        .configurator .t-dropdown
        {
            vertical-align: middle;
        }
    
    </style>
</asp:Content>
