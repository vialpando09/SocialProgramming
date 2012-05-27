// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.UI.Html;

    public class GridEditActionCommand : GridActionCommandBase
    {
        public override string Name
        {
            get { return "edit"; }
        }

        public override IEnumerable<IGridButtonBuilder> CreateDisplayButtons(IGridLocalization localization, IGridUrlBuilder urlBuilder, IGridHtmlHelper htmlHelper)
        {
            var editButton = CreateButton<GridLinkButtonBuilder>(localization.Edit, UIPrimitives.Grid.Edit);

            editButton.Url = urlBuilder.EditUrl;

            editButton.SpriteCssClass = "t-edit";

            return new[]
            {
                editButton
            };
        }

        public override IEnumerable<IGridButtonBuilder> CreateEditButtons(IGridLocalization localization, IGridUrlBuilder urlBuilder, IGridHtmlHelper htmlHelper)
        {
            var cancelButton = CreateButton<GridLinkButtonBuilder>(localization.Cancel, UIPrimitives.Grid.Cancel);

            cancelButton.Url = urlBuilder.CancelUrl;
            
            cancelButton.SpriteCssClass = "t-cancel";

            var updateButton = CreateButton<GridButtonBuilder>(localization.Update, UIPrimitives.Grid.Update);
            updateButton.ShouldAppendDataKeys = true;
            updateButton.SpriteCssClass = "t-update";
            updateButton.HtmlHelper = htmlHelper;

            return new IGridButtonBuilder[]
            {
                updateButton,
                cancelButton
            };
        }

        public override IEnumerable<IGridButtonBuilder> CreateInsertButtons(IGridLocalization localization, IGridUrlBuilder urlBuilder, IGridHtmlHelper htmlHelper)
        {
            var cancelButton = CreateButton<GridLinkButtonBuilder>(localization.Cancel, UIPrimitives.Grid.Cancel);

            cancelButton.Url = urlBuilder.CancelUrl;

            cancelButton.SpriteCssClass = "t-cancel";

            var insertButton = CreateButton<GridButtonBuilder>(localization.Insert, UIPrimitives.Grid.Insert);
            insertButton.SpriteCssClass = "t-insert";
            insertButton.HtmlHelper = htmlHelper;

            return new IGridButtonBuilder[]
            {
                insertButton,
                cancelButton
            };
        }
    }
}
