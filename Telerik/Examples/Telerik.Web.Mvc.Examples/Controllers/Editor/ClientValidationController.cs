namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class EditorController : Controller
    {
        [SourceCodeFile("EmployeeDto (model)", "~/Models/EmployeeDto.cs")]
        public ActionResult ClientValidation()
        {
            var employee = new EmployeeDto
               {
                   FirstName = "Nancy",
                   LastName = "Davolio",
                   Notes = ""
               };

            return View(employee);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [SourceCodeFile("EmployeeDto (model)", "~/Models/EmployeeDto.cs")]
        public ActionResult ClientValidation(EmployeeDto employee)
        {
            employee.Notes = employee.Notes;
            return View(employee);
        }
    }
}