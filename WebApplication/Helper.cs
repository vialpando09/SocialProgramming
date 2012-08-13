using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc.UI;
using System.Linq.Expressions;


namespace WebApplication
{
    public static class Helpers
    {
        public static HtmlString FullTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string content = "";
            ModelMetadata data = ModelMetadata.FromLambdaExpression(expression, html.ViewData as ViewDataDictionary<TModel>);
            if (data.Model != null)
                content = data.Model.ToString();

            return new HtmlString(
                html.Telerik().Editor()
                    .Name(data.PropertyName)
                    .Value(HttpUtility.HtmlDecode(content))
                    .Tools(tools => tools.Clear()
                        .FormatBlock()
                        .FontName()
                        .FontSize()
                        .FontColor()
                        .BackColor()
                        .Separator()
                        .Bold()
                        .Italic()
                        .Underline()
                        .Separator()
                        .Indent()
                        .Outdent()
                        .Separator()
                        .InsertOrderedList()
                        .InsertUnorderedList()
                        .Break()
                        .JustifyLeft()
                        .JustifyCenter()
                        .JustifyRight()
                        .JustifyFull()
                        .Separator()
                        .Subscript()
                        .Superscript()
                        .Separator()
                        .CreateLink()
                        .Unlink()
                        .InsertImage()
                        .Styles(styles => styles.Add("Highlight", "highlight").Add("Code block", "code").Add("Keybinding", "keybinding"))
                        )
                    .StyleSheets(styleSheets => styleSheets.Add("~/Content/Admin.css"))            
                    .FileBrowser(t =>   t.Browse("Browse", "FileBrowser")
                                         .Thumbnail("Thumbnail", "FileBrowser")
                                         .Upload("Upload", "FileBrowser")
                                         .DeleteFile("DeleteFile", "FileBrowser")
                                         .DeleteDirectory("DeleteDirectory", "FileBrowser")
                                         .CreateDirectory("CreateDirectory", "FileBrowser")).ToHtmlString());
        }
        public static HtmlString SimpleTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string content = "";
            ModelMetadata data = ModelMetadata.FromLambdaExpression(expression, html.ViewData as ViewDataDictionary<TModel>);
            if (data.Model != null)
                content = data.Model.ToString();

            return new HtmlString(
                html.Telerik().Editor()
                    .Name("Editor")
                    .Value(content)
                    .Tools(tools => tools.Clear()
                        .Bold()
                        .Italic()
                        .Underline()
                        .Separator()
                        .Indent()
                        .Outdent()
                        .Separator()
                        .InsertOrderedList()
                        .InsertUnorderedList()
                        .JustifyLeft()
                        .JustifyCenter()
                        .JustifyRight()
                        .JustifyFull()
                        .Separator()
                        .Subscript()
                        .Superscript()
                        .Separator()
                        .CreateLink()
                        .Unlink()
                        )
                    .ToHtmlString());
        }
    }
}