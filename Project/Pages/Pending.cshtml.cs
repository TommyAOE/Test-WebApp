using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Project.Pages;

[Authorize(Roles = "Pending")]
public class PendingModel : PageModel
{
    private readonly ILogger<PendingModel> _logger;

    public PendingModel(ILogger<PendingModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

