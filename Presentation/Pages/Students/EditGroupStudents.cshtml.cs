using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Reports
{
    public class EditGroupStudentsModel : PageModel
    {
        public static GroupStudents Group = new();
        public void OnGet()
        {
        }

        public static void GetView(GroupStudents group, HttpResponse response)
        {
            Group = group;
            response.Redirect("/Students/EditGroupStudents");
        }

        public void OnPostPutLastReport()
        {
        }

        public void OnPostAddReport()
        {

        }
    }
}
