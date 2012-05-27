namespace Telerik.Web.Mvc.Examples
{
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.Extensions;

    public partial class GridController
    {
        [SourceCodeFile("EditableOrder (model)", "~/Models/EditableOrder.cs")]
        [SourceCodeFile("Employee (model)", "~/Models/Employee.cs")]
        [SourceCodeFile(
            Caption = "Employee (Display)",
            FileName = "~/Views/Grid/DisplayTemplates/Employee.ascx",
            RazorFileName = "~/Areas/Razor/Views/Grid/DisplayTemplates/Employee.cshtml")]
        [SourceCodeFile(
            Caption = "Employee (Editor)",
            FileName = "~/Views/Grid/EditorTemplates/Employee.ascx",
            RazorFileName = "~/Areas/Razor/Views/Grid/EditorTemplates/Employee.cshtml")]
        public ActionResult ServerEditTemplates()
        {
            PopulateEmployees();

            return View(SessionOrderRepository.All());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateOrder(int id, int employee)
        {
            EditableOrder order = new EditableOrder
            {
                OrderID = id,
                Employee = new NorthwindDataContext().Employees.SingleOrDefault(e => e.EmployeeID == employee)
            };

            // Exclude "Employee" from the list of updated properties
            if (TryUpdateModel(order, null, null, new [] { "Employee" }))
            {
                SessionOrderRepository.Update(order);

                return RedirectToAction("ServerEditTemplates", this.GridRouteValues());
            }

            PopulateEmployees();

            return View("ServerEditTemplates", SessionOrderRepository.All());
        }

        private void PopulateEmployees()
        {
            ViewData["employees"] = new NorthwindDataContext().Employees
                                                              .Select(e => new { Id = e.EmployeeID, Name = e.FirstName + " " + e.LastName })
                                                              .OrderBy(e => e.Name);
        }
    }
}