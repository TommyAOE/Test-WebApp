using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project;

[Authorize(Roles = "Admin, Employee, Viewer")]
public class Event
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        // Other properties as needed
    }
public class CalendarModel : PageModel
{
    private readonly ILogger<CalendarModel> _logger;
    private readonly Project.Data.LeaveRequestDbContext _context;
    [BindProperty]
    public Project.Data.LeaveRequest Model { get; set; }
    public List<Project.Data.LeaveRequest> LeaveRequests { get; set; }

    public CalendarModel(ILogger<CalendarModel> logger, Project.Data.LeaveRequestDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        LeaveRequests = _context.LeaveRequests.ToList();
    }
    public JsonResult OnGetEvents()
    {
        // Fetch events from your data source (e.g., database)
        var events = new List<Event>
        {
            new Event { Title = "Event 1", Start = DateTime.Now.AddDays(1) },
            new Event { Title = "Event 2", Start = DateTime.Now.AddDays(2) }
            // Add more events as needed
        };

        return new JsonResult(events);
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

