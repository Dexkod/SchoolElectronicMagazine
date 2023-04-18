using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Reports
{
    public class CreateReportModel : PageModel
    {
        public List<Report> Reports = new();
        private SchoolContext context = new();

        public CreateReportModel()
        {
            Reports = context.Reports.ToList();
        }
        public void OnGet()
        {
        }
    }
}
