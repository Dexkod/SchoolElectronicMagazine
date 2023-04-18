using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Presentation.Pages.Reports
{
    public class AllGroupStudent: PageModel
    {
        public string ErrorMessage = "";
        public string ValidationString = "";
        public List<GroupStudents> groupStudents = new();
        private SchoolContext context = new();
        public AllGroupStudent() 
        {
            groupStudents = context.GroupsStudents.ToList();
        }
        public void OnGet(){}

        public void OnPostRemoveGroup(string GroupId) 
        {
            GroupStudents groupStudents = context.GroupsStudents
                .FirstOrDefault(x => x.Id.ToString().Equals(GroupId));
            context.Remove(groupStudents);

            context.SaveChanges();

            Response.Redirect(Request.Path);
        }

        public void OnPostMoreDetails(string GroupId)
        {
            var group = context.GroupsStudents.FirstOrDefault(x => x.Id.ToString().Equals(GroupId));
            EditGroupStudentsModel.GetView(group, Response);
        }

        public void OnPostSearchGroup(string? NameGroup)
        {
            ValidationString = NameGroup == null ? "" : NameGroup;
        }

        public void OnPostAddGroup(string NameGroup)
        {
            if (NameGroup.IsNullOrEmpty())
            {
                ErrorMessage = "При создании произошла ошибка, введите имя";
                return;
            }
            else if (context.GroupsStudents.Any(x => x.Name.Equals(NameGroup)))
            {
                ErrorMessage = "При создании произошла ошибка, такое имя уже существует";
                return;
            }
            ErrorMessage = "";

            context.GroupsStudents.Add(new GroupStudents(NameGroup));
            context.SaveChanges();
            Response.Redirect(Request.Path);
        }
    }
}
