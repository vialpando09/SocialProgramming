namespace Telerik.Web.Mvc.JavaScriptTests.Extensions
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    public static class HtmlHelperExtensions
    {
        static public HtmlHelper RegisterSplitterScripts(this HtmlHelper helpers)
        {
            helpers.Telerik().ScriptRegistrar()
                .DefaultGroup(defaultGroup => defaultGroup
                    .Add("telerik.common.js")
                    .Add("telerik.draganddrop.js")
                    .Add("telerik.splitter.js")
                    .Add("splitterTestHelper.js")
                );

            return helpers;
        }
    }
}