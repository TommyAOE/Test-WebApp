using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGetAsync()
    {
        if(User.IsInRole("Admin")){
            return RedirectToPage("Admin");
        }else if(User.IsInRole("Pending")){
            return RedirectToPage("Pending");
        }else if(User.IsInRole("Employee")){
            return RedirectToPage("Calendar");
        }else if(User.IsInRole("Viewer")){
            return RedirectToPage("Calendar");
        }else{
            return RedirectToPage("Calendar");
        }
    }
}
