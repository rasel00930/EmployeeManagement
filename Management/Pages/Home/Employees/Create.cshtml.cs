using Management.Models;
using Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Management.Pages.Home.Employees
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public EmployeeDto EmployeeDto { get; set; } = new EmployeeDto();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            if (EmployeeDto.Photo == null)
            {
                ModelState.AddModelError("EmployeeDto.Photo", "The photo is required.");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(EmployeeDto.Photo!.FileName);

            string photoFullPath = environment.WebRootPath + "/employees/" + newFileName;
            using (var stream = System.IO.File.Create(photoFullPath))
            {
                EmployeeDto.Photo.CopyTo(stream);
            }

           
            Employee employee = new Employee()
            {
                FirstName = EmployeeDto.FirstName,
                LastName = EmployeeDto.LastName,
                Email = EmployeeDto.Email,
                Mobile = EmployeeDto.Mobile,
                DateOfBirth = EmployeeDto.DateOfBirth,
                ImagePath = newFileName,
                
            };

            context.Employees.Add(employee);
            context.SaveChanges();

           
            EmployeeDto.FirstName = "";
            EmployeeDto.LastName = "";
            EmployeeDto.Email = "";
            EmployeeDto.Mobile = "";
            EmployeeDto.DateOfBirth = DateTime.Now;
            EmployeeDto.Photo = null;

            ModelState.Clear();

            successMessage = "Employee created successfully";

            Response.Redirect("/Home/Employees/Index");
        }
    }
}
