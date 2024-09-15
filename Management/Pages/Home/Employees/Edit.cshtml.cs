using Management.Models;
using Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Management.Pages.Home.Employees
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public EmployeeDto EmployeeDto { get; set; } = new EmployeeDto();

        public Employee Employee { get; set; } = new Employee();

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
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

           
            EmployeeDto.FirstName = employee.FirstName;
            EmployeeDto.LastName = employee.LastName;
            EmployeeDto.Email = employee.Email;
            EmployeeDto.Mobile = employee.Mobile;
            EmployeeDto.DateOfBirth = employee.DateOfBirth;

            Employee = employee;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Home/Employees/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                Response.Redirect("/Admin/Employees/Index");
                return;
            }

            
            string newFileName = employee.ImagePath;
            if (EmployeeDto.Photo != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(EmployeeDto.Photo.FileName);

                string photoFullPath = environment.WebRootPath + "/employees/" + newFileName;
                using (var stream = System.IO.File.Create(photoFullPath))
                {
                    EmployeeDto.Photo.CopyTo(stream);
                }

               
                string oldPhotoFullPath = environment.WebRootPath + "/employees/" + employee.ImagePath;
                System.IO.File.Delete(oldPhotoFullPath);
            }

            
            employee.FirstName = EmployeeDto.FirstName;
            employee.LastName = EmployeeDto.LastName;
            employee.Email = EmployeeDto.Email;
            employee.Mobile = EmployeeDto.Mobile;
            employee.DateOfBirth = EmployeeDto.DateOfBirth;
            employee.ImagePath = newFileName;

            context.SaveChanges();

            Employee = employee;
            successMessage = "Employee updated successfully";

            Response.Redirect("/Home/Employees/Index");
        }
    }
}
