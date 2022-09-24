namespace Domain.Dtos;

public class GetManagerDto
{
    public int ManagerId { get; set; }
    public string ManagerFullName { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; private set; }
}
