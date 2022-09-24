namespace Domain.Dtos;

public class GetDepartmentsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ManagerId { get; set; }
    public string ManagerFullName { get; set; }
}
