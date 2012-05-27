<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Nullable<int>>" %>

<%= Html.Telerik().IntegerTextBox()
            .Name( ViewData.TemplateInfo.GetFullHtmlFieldName( string.Empty ) )
            .InputHtmlAttributes( new { style = "width:100%" } )
            .EmptyMessage( string.Empty )
            .MinValue( int.MinValue )
            .MaxValue( int.MaxValue )
            .Value( Model )
%>
 