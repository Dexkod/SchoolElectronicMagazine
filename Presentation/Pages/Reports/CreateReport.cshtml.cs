using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Reports
{
    public class CreateReportModel : PageModel
    {
        public List<Report> Reports = new();
        public List<GroupStudents> Groups = new();
        public List<Teacher> Teachers = new();
        private SchoolContext context = new();

        public CreateReportModel()
        {
            Reports = context.Reports.ToList();
            Groups = context.GroupsStudents.ToList();
            Teachers = context.Teachers.ToList();
        }
        public void OnGet()
        {
        }

        public void OnPostCreateReport(string NameReport, string GroupId, string TeacherId)
        {
        }
    }
}
