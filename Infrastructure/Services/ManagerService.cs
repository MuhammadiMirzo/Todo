namespace Infrastructure.Services;
    using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;

public class ManagerService
{
     private DataContext.DataContext _context;
    public ManagerService(DataContext.DataContext context)
    {
        _context = context;
    }


    public async Task<Response<List<GetManagerDto>>> GetManagerDto()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetManagerDto>($"SELECT E.Id as {"ManagerId"} ,CONCAT(E.FirstName, ' ', E.LastName) AS {"ManagerFullName"}, D.Id as {"DepartmentId"}, D.Name as {"DepartmentName"}, O.FromDate,O.ToDate FROM department AS D INNER JOIN Department_employee AS O ON O.DepartmentId = D.Id INNER JOIN employee AS E ON O.EmployeeId = E.Id;");
                return new Response<List<GetManagerDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetManagerDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    
}

