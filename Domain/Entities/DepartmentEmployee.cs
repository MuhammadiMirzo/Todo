namespace Domain.Entities;

public class DepartmentEmployee
{
     public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool CurrentDepartment { get; set; }
}
