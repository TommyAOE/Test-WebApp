using Microsoft.EntityFrameworkCore;

namespace Project.Data;
public class LeaveRequestDbContext : DbContext
{
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public LeaveRequestDbContext(DbContextOptions options)
        : base(options)
    {
    }
}