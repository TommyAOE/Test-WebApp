using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Project.Pages;

[Authorize(Roles = "Admin, Employee, Viewer")]
public class CalendarModel : PageModel
{
    private readonly ILogger<CalendarModel> _logger;

    public CalendarModel(ILogger<CalendarModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

