using Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Pages.Home.Employees
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext context;
        private string? column;
        private string? orderBy;

        public List<Models.Employee> Employees { get; set; } = new List<Models.Employee>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; } 

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEmail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchMobile { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchDOB { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int? pageNumber, string? column, string? orderBy)
        {
            CurrentPage = pageNumber ?? 1;

            var query = context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(SearchName))
            {
                query = query.Where(e => e.FirstName.Contains(SearchName) || e.LastName.Contains(SearchName));
            }

            if (!string.IsNullOrEmpty(SearchEmail))
            {
                query = query.Where(e => e.Email.Contains(SearchEmail));
            }

            if (!string.IsNullOrEmpty(SearchMobile))
            {
                query = query.Where(e => e.Mobile.Contains(SearchMobile));
            }

            if (!string.IsNullOrEmpty(SearchDOB) && DateTime.TryParse(SearchDOB, out DateTime dob))
            {
                query = query.Where(e => e.DateOfBirth.Date == dob.Date);
            }
            TotalRecords = query.Count();


            string[] validColumns = { "FirstName", "Email", "Mobile", "DateOfBirth" };
            string[] validOrderBy = { "desc", "asc" };

            if (!validColumns.Contains(column))
            {
                column = "FirstName";
            }

            if (!validOrderBy.Contains(orderBy))
            {
                orderBy = "desc";
            }

            this.column = column;
            this.orderBy = orderBy;

            if (column == "FirstName")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.FirstName);
                }
                else
                {
                    query = query.OrderByDescending(p => p.FirstName);
                }
            }
            else if (column == "Email")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Email);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Email);
                }
            }
            else if (column == "Mobile")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Mobile);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Mobile);
                }
            }
            else if (column == "DateOfBirth")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.DateOfBirth);
                }
                else
                {
                    query = query.OrderByDescending(p => p.DateOfBirth);
                }
            }
            else
            {
                // Default sort by FullName if an unknown column is provided
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.FirstName);
                }
                else
                {
                    query = query.OrderByDescending(p => p.FirstName);
                }
            }

            Employees = context.Employees.OrderBy(e => e.FirstName).ToList();

            TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);

            Employees = query
                        .OrderBy(e => e.Id)
                        .Skip((CurrentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
        }
    }

}
