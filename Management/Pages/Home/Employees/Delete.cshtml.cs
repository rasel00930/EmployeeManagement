using Management.Models;
using Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Management.Pages.Home.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Employees/Index");
                return;
            }

            
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                Response.Redirect("/Home/Employees/Index");
                return;
            }

           
            string photoFullPath = Path.Combine(environment.WebRootPath, "Employees", employee.ImagePath);
            if (System.IO.File.Exists(photoFullPath))
            {
                System.IO.File.Delete(photoFullPath);
            }

           
            context.Employees.Remove(employee);
            context.SaveChanges();

            
            Response.Redirect("/Home/Employees/Index");
        }
    }

}
