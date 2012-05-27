namespace Telerik.Web.Mvc.Examples
{
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.Extensions;

    public partial class EditorController : Controller
    {
        [SourceCodeFile("EditableEmployee (model)", "~/Models/EditableEmployee.cs")]
        [SourceCodeFile(
            FileName = "~/Views/Editor/EditorTemplates/Editor.ascx",
            RazorFileName = "~/Areas/Razor/Views/Editor/EditorTemplates/Editor.cshtml")]
        public ActionResult EditorInGrid()
        {
            return View(SessionEmployeeRepository.All());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEmployee(int id)
        {
            EditableEmployee employee = SessionEmployeeRepository.One(e => e.EmployeeID == id);

            if (TryUpdateModel(employee))
            {
                // HTML decode the Notes property
                employee.Notes = HttpUtility.HtmlDecode(employee.Notes);
                SessionEmployeeRepository.Update(employee);

                return RedirectToAction("EditorInGrid", this.GridRouteValues());
            }

            return View("EditorInGrid", SessionEmployeeRepository.All());
        }
    }
}