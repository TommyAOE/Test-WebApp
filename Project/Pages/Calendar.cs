using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project;

[Authorize(Roles = "Admin, Employee, Viewer")]
public class CalendarModel : PageModel
{
    private readonly ILogger<CalendarModel> _logger;
    private readonly Project.Data.LeaveRequestDbContext _context;
    [BindProperty]
    public Project.Data.LeaveRequest Model { get; set; }

    public CalendarModel(ILogger<CalendarModel> logger, Project.Data.LeaveRequestDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
    }
    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _context.LeaveRequests.Add(Model);
            _context.SaveChanges();
            return Page(); // Redirect to the index page after successful submission
        }
        return Page(); // If model state is not valid, stay on the same page
    }
}

