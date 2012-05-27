<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime>" %>

<%= Html.Telerik().DatePicker()
        .Name(ViewData.TemplateInfo.HtmlFieldPrefix)
        .Value(Model > DateTime.MinValue? Model : DateTime.Today)
%>