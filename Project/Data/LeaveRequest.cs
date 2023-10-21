using System.ComponentModel.DataAnnotations;

namespace Project.Data;

public class LeaveRequest
{
    public int Id { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public bool Approved { get; set; }
}