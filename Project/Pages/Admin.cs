using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
public class UserRolesViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public List<string> AvailableRoles { get; set; }
}
public class AdminModel : PageModel
{
    public readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    [BindProperty]
    public List<UserRolesViewModel> UsersWithRoles { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        
        var users = await _userManager.Users.ToListAsync();
        var availableRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

        UsersWithRoles = new List<UserRolesViewModel>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            UsersWithRoles.Add(new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.ToList(),
                AvailableRoles = availableRoles
            });
        }
        TempData["UsersWithRoles"] = JsonConvert.SerializeObject(UsersWithRoles);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Retrieve data from TempData
        var usersWithRolesJson = TempData["UsersWithRoles"] as string;

        // Deserialize the JSON string back to the original object
        var usersWithRoles = JsonConvert.DeserializeObject<List<UserRolesViewModel>>(usersWithRolesJson);

        // Check for null, then perform operations on usersWithRoles...
        if (!ModelState.IsValid)
        {
            return Page();
        }
        int i = 0;
        foreach (var userWithRoles in usersWithRoles)
        {
            var user = await _userManager.FindByIdAsync(userWithRoles.UserId);
            if (user != null )
            {
                if(Request.Form["Roles[" + i + "]"].ToString() != "")
                userWithRoles.Roles = Request.Form["Roles[" + i + "]"]
                                        .ToString()
                                        .Split(",")
                                        .ToList();
                // Remove existing roles and add selected roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, existingRoles);
                await _userManager.AddToRolesAsync(user, userWithRoles.Roles);
            }
            i++;
        }
        // Redirect to a success page or return to the same page with a success message
        return RedirectToPage("Index"); // Redirect to the desired page
    }
}