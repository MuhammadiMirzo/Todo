namespace Domain.Dtos;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ManagerId { get; set; }
    public string ManagerFullName { get; set; }
}
