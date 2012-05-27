<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Employee>" %>

<%= Html.Telerik().DropDownList()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .BindTo(new SelectList((IEnumerable) ViewData["employees"], "Id", "Name", Model.EmployeeID.ToString()))
%>